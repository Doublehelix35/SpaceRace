using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FoodSpawner : MonoBehaviour {

    public GameObject[] Spawners; // Must have 5 gameobjects
    GameObject[] ActiveFood; // Stores active food objects
    public GameObject FoodObject; // Food prefab

    // Food objects
    GameObject Food1 = null;
    GameObject Food2 = null;
    GameObject Food3 = null;
    GameObject Food4 = null;
    GameObject Food5 = null;

    float CountDown = 6f; // Time until food spawns

	void Start ()
    {
        // Spawn food 3
        SpawnFood(Spawners[2], ref Food3, 3);
    }
	
	void Update ()
    {
        // Update timer
        CountDown -= Time.deltaTime; 

        if(CountDown <= 0) // Check timer
        {
            // Select food to spawn
            int RandSelect = Random.Range(1, 6); 
            switch (RandSelect)
            {
                case 1:
                    if(Food1 == null) // Only spawn if null
                    {
                        SpawnFood(Spawners[0], ref Food1, 1);
                    }
                    break;
                case 2:
                    if (Food2 == null) // Only spawn if null
                    {
                        SpawnFood(Spawners[1], ref Food2, 2);
                    }
                    break;
                case 3:
                    if (Food3 == null) // Only spawn if null
                    {
                        SpawnFood(Spawners[2], ref Food3, 3);
                    }
                    break;
                case 4:
                    if (Food4 == null) // Only spawn if null
                    {
                        SpawnFood(Spawners[3], ref Food4, 4);
                    }
                    break;                    
                case 5:
                    if (Food5 == null) // Only spawn if null
                    {
                        SpawnFood(Spawners[4], ref Food5, 5);
                    }
                    break;
                default:
                    Debug.Log("ERROR NO FOOD FOUND!");
                    break;
            }
            CountDown = 6f; // Reset countdown
        }
	}

    void SpawnFood(GameObject Spawner, ref GameObject FoodRef, int IdNum)
    {
        // Set spawn position
        Vector3 spawnPosition = new Vector3(Spawner.transform.position.x, Spawner.transform.position.y, 0);

        // Spawn food and store the ref in the gameobject passed in
        FoodRef = Instantiate(FoodObject, spawnPosition, Quaternion.identity);

        // Tell the food its id number
        FoodRef.GetComponent<S_Food>().SetIdNum(IdNum);
        
    }

    public void SetFoodToNull(int IdNum)
    {
        // Set food to null if id is passed in
        if(IdNum == 1)
        {
            Food1 = null;
        }
        else if(IdNum == 2)
        {
            Food2 = null;
        }
        else if (IdNum == 3)
        {
            Food3 = null;
        }
        else if (IdNum == 4)
        {
            Food4 = null;
        }
        else if (IdNum == 5)
        {
            Food5 = null;
        }

    }
}
