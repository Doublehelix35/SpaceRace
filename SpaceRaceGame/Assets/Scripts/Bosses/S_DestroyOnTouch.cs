using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DestroyOnTouch : MonoBehaviour {

    GameObject SnakeManagerRef;
    float RespawnTimer = 0.1f;

    bool CanSpawn = false;


    void Start()
    {
        SnakeManagerRef = GameObject.Find("SnakeManager");
    }

    void Update()
    {
        RespawnTimer -= Time.deltaTime;

        if (RespawnTimer <= 0)
        {
            CanSpawn = true;
            RespawnTimer = 0.1f;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Boss")
        {
            //Debug.Log(col.gameObject.name);
            if (!CanSpawn)
            {
                return;
            }

            GameObject SnakeCol = col.gameObject;

            if (SnakeCol.GetComponent<S_SnakeBoss>().SurvivedOnce)
            {
                SnakeManagerRef.GetComponent<S_SnakeManager>().NextSpawn();
                CanSpawn = false;
                Destroy(col.transform.parent.gameObject, 5f);
            }

            SnakeCol.GetComponent<S_SnakeBoss>().SurvivedOnce = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boss")
        {
            //Debug.Log(col.gameObject.name);
            if (!CanSpawn)
            {
                return;
            }

            GameObject SnakeCol = col.gameObject;

            if (SnakeCol.GetComponent<S_SnakeBoss>().SurvivedOnce)
            {
                SnakeManagerRef.GetComponent<S_SnakeManager>().NextSpawn();
                CanSpawn = false;
                Destroy(col.transform.parent.gameObject, 5f);
            }

            SnakeCol.GetComponent<S_SnakeBoss>().SurvivedOnce = true;
        }
    }
}
