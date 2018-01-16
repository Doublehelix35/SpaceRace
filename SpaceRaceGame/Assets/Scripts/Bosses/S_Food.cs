using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Food : MonoBehaviour {

    GameObject FoodManagerRef;
    GameObject SnakeManagerRef;
    public int Health = 1;
    int idNum = 10000;

    private void Start()
    {
        FoodManagerRef = GameObject.Find("FoodSpawner");
        SnakeManagerRef = GameObject.Find("SnakeManager");
    }

    public void SetIdNum(int num)
    {
        idNum = num;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Bullet")
        {

            Health--;

            if (Health <= 0)
            {
                FoodManagerRef.GetComponent<S_FoodSpawner>().SetFoodToNull(idNum);
                Destroy(gameObject, 0f);
            }
        }

        if (col.gameObject.tag == "Boss")
        {
            SnakeManagerRef.GetComponent<S_SnakeManager>().IncreaseSnakeSize();
            FoodManagerRef.GetComponent<S_FoodSpawner>().SetFoodToNull(idNum);
            Destroy(gameObject, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Bullet")
        {
            
            Health--;

            if (Health <= 0)
            {
                FoodManagerRef.GetComponent<S_FoodSpawner>().SetFoodToNull(idNum);
                Destroy(gameObject, 0f);
            }
        }

        if (col.gameObject.tag == "Boss")
        {
            SnakeManagerRef.GetComponent<S_SnakeManager>().IncreaseSnakeSize();
            FoodManagerRef.GetComponent<S_FoodSpawner>().SetFoodToNull(idNum);
            Destroy(gameObject, 0f);
        }
    }
}
