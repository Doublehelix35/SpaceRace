using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DestroyOnTouch : MonoBehaviour {

    GameObject SnakeManagerRef;
    float RespawnTimer = 0.1f; // Time until respawn

    bool CanSpawn = false; // Able to spawn or not


    void Start()
    {
        SnakeManagerRef = GameObject.Find("SnakeManager");
    }

    void Update()
    {
        // Update timer
        RespawnTimer -= Time.deltaTime;

        // Check if can respawn
        if (RespawnTimer <= 0)
        {
            CanSpawn = true;
            RespawnTimer = 0.1f; // Reset timer
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Boss")
        {
            // Exit function if cant spawn
            if (!CanSpawn)
            {
                return;
            }

            
            GameObject SnakeCol = col.gameObject;

            if (SnakeCol.GetComponent<S_SnakeBoss>().SurvivedOnce) // Check if snake has survived once
            {
                SnakeManagerRef.GetComponent<S_SnakeManager>().SpawnBoss(); // Spawn next snake
                CanSpawn = false;
                Destroy(col.transform.parent.gameObject, 5f); // Destroy col's parent after delay
            }

            SnakeCol.GetComponent<S_SnakeBoss>().SurvivedOnce = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boss")
        {
            // Exit function if cant spawn
            if (!CanSpawn)
            {
                return;
            }

            GameObject SnakeCol = col.gameObject;

            if (SnakeCol.GetComponent<S_SnakeBoss>().SurvivedOnce) // Check if snake has survived once
            {
                SnakeManagerRef.GetComponent<S_SnakeManager>().SpawnBoss(); // Spawn next snake
                CanSpawn = false;
                Destroy(col.transform.parent.gameObject, 5f); // Destroy col's parent after delay
            }

            SnakeCol.GetComponent<S_SnakeBoss>().SurvivedOnce = true;
        }
    }
}
