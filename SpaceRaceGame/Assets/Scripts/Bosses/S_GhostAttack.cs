using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GhostAttack : MonoBehaviour {

    int phase = 1;

    public float MinTimeTilDrop = 2f;
    public float MaxTimeTilDrop = 10f;
    float RandTimeTilDrop;
    float LastDropTime;
    public float RespawnTime = 3f;
    bool AbleToDrop = true;

    public GameObject PlayerRef;

    public float DropSpeed = 1f;
    public float Health = 5f;
    float HealthMax;

	// Use this for initialization
	void Start ()
    {
        // Initialize values
        LastDropTime = Time.time;
        RandTimeTilDrop = Random.Range(MinTimeTilDrop, MaxTimeTilDrop);
        HealthMax = Health;
	}
	
	// Update is called once per frame
	void Update ()
    {
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

            if (Health <= 0)
            {
                // Hide and move
                gameObject.GetComponent<SpriteRenderer>().enabled = false; // Hide
                gameObject.GetComponent<S_Follow>().enabled = true; // Follow boss again
                gameObject.GetComponent<Collider2D>().enabled = false; // Turn off collision
                AbleToDrop = false;

                StartCoroutine("GhostRespawn");
            }
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            // Hide and move
            gameObject.GetComponent<SpriteRenderer>().enabled = false; // Hide
            gameObject.GetComponent<S_Follow>().enabled = true; // Follow boss again
            gameObject.GetComponent<Collider2D>().enabled = false; // Turn off collision
            AbleToDrop = false;

            StartCoroutine("GhostRespawn");
            
        }
    }

    IEnumerator GhostRespawn()
    {
        yield return new WaitForSeconds(RespawnTime);

        gameObject.GetComponent<SpriteRenderer>().enabled = true; // Un-hide

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

        float FadeProgress = 0.0f;

        while (FadeProgress < 1)
        {
            TempColor.a = Mathf.Lerp(0f, 255f, FadeProgress);
            gameObject.GetComponent<SpriteRenderer>().color = TempColor;

            FadeProgress += Time.deltaTime;
            yield return null;
        }
    }
}
