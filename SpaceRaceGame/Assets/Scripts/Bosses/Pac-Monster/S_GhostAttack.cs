using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GhostAttack : MonoBehaviour {

    int phase = 1;

    public float MinTimeTilDrop = 2f; // Min time until ghost attacks player
    public float MaxTimeTilDrop = 10f; // Max time until ghost attacks player
    float RandTimeTilDrop; // Time til ghost attacks player
    float LastDropTime; // Time.time of last attack
    public float RespawnTime = 3f; // Time until respawn
    bool AbleToDrop = true;

    public GameObject PlayerRef;

    public float DropSpeed = 1f; // Speed of ghost
    public float Health = 5f; // Current health
    float HealthMax; // Max health

	void Start ()
    {
        // Initialize values
        LastDropTime = Time.time;
        RandTimeTilDrop = Random.Range(MinTimeTilDrop, MaxTimeTilDrop);
        HealthMax = Health;
	}

	void Update ()
    {
        // Check if ghost can drop
        if(LastDropTime + RandTimeTilDrop < Time.time && AbleToDrop)
        {
            // Stop following boss
            gameObject.GetComponent<S_Follow>().enabled = false;

            // Move to player position
            transform.position = Vector2.MoveTowards(transform.position, PlayerRef.transform.position, DropSpeed * Time.deltaTime);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            //Lose health
            Health--;

            if (Health <= 0) // Check health
            {
                StartCoroutine("GhostRespawn");
            }
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine("GhostRespawn");
            
        }
    }

    IEnumerator GhostRespawn()
    {
        // Set alpha to 0
        Color TempColor = gameObject.GetComponent<SpriteRenderer>().color;
        TempColor.a = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = TempColor;

        gameObject.GetComponent<S_Follow>().enabled = true; // Follow boss again
        gameObject.GetComponent<Collider2D>().enabled = false; // Turn off collision
        AbleToDrop = false;

        yield return new WaitForSeconds(RespawnTime);

        StartCoroutine("FadeIn");

        yield return new WaitForSeconds(0.3f);

        gameObject.GetComponent<Collider2D>().enabled = true; // Turn on collision
        Health = HealthMax; // Reset health
        LastDropTime = Time.time; // Reset timer
        AbleToDrop = true;
    }

    IEnumerator FadeIn()
    {
        // Create temp color for fade in
        Color TempColor = gameObject.GetComponent<SpriteRenderer>().color;

        float FadeProgress = 0.0f; // Percentage of progress

        while (FadeProgress < 1)
        {
            // Smoothly go from 0 to 100% alpha
            TempColor.a = Mathf.Lerp(0f, 1f, FadeProgress);
            gameObject.GetComponent<SpriteRenderer>().color = TempColor;
       
            FadeProgress += Time.deltaTime * 0.8f; // Update fade progress
            yield return null;
        }
    }
}
