using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Food : MonoBehaviour {

    GameObject FoodManagerRef;
    GameObject SnakeManagerRef;
    int idNum = 10000; // Error has occured if id is still equal to this

    void Start()
    {
        // Initialize values
        FoodManagerRef = GameObject.Find("FoodSpawner");
        SnakeManagerRef = GameObject.Find("SnakeManager");
    }

    public void SetIdNum(int num)
    {
        // Set id of food
        idNum = num;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Bullet")
        {
            // Tell food manager this id is empty and then destroy gameobject
            FoodManagerRef.GetComponent<S_FoodSpawner>().SetFoodToNull(idNum);
            Destroy(gameObject, 0f);
        }

        if (col.gameObject.tag == "Boss")
        {
            // Increase snake size
            SnakeManagerRef.GetComponent<S_SnakeManager>().IncreaseSnakeSize();

            // Tell food manager this id is empty and then destroy gameobject
            FoodManagerRef.GetComponent<S_FoodSpawner>().SetFoodToNull(idNum);
            Destroy(gameObject, 0f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Bullet")
        {
            // Tell food manager this id is empty and then destroy gameobject
            FoodManagerRef.GetComponent<S_FoodSpawner>().SetFoodToNull(idNum);
            Destroy(gameObject, 0f);
        }

        if (col.gameObject.tag == "Boss")
        {
            // Increase snake size
            SnakeManagerRef.GetComponent<S_SnakeManager>().IncreaseSnakeSize();

            // Tell food manager this id is empty and then destroy gameobject
            FoodManagerRef.GetComponent<S_FoodSpawner>().SetFoodToNull(idNum);
            Destroy(gameObject, 0f);
        }
    }
}
