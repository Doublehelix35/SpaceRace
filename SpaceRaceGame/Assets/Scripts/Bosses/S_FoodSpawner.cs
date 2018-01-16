using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FoodSpawner : MonoBehaviour {

    public GameObject[] Spawners; // Must have 5 gameobjects
    GameObject[] ActiveFood;
    public GameObject FoodObject;

    GameObject Food1 = null;
    GameObject Food2 = null;
    GameObject Food3 = null;
    GameObject Food4 = null;
    GameObject Food5 = null;

    float CountDown = 6f;

	// Use this for initialization
	void Start () {
        // Spawn food 1 & 3 & 5
        SpawnFood(Spawners[2], ref Food3, 3);
    }
	
	// Update is called once per frame
	void Update () {
        CountDown -= Time.deltaTime;

        if(CountDown <= 0)
        {
            int RandSelect = Random.Range(1, 6);
            switch (RandSelect)
            {
                case 1:
                    if(Food1 == null)
                    {
                        SpawnFood(Spawners[0], ref Food1, 1);
                    }
                    break;
                case 2:
                    if (Food2 == null)
                    {
                        SpawnFood(Spawners[1], ref Food2, 2);
                    }
                    break;
                case 3:
                    if (Food3 == null)
                    {
                        SpawnFood(Spawners[2], ref Food3, 3);
                    }
                    break;
                case 4:
                    if (Food4 == null)
                    {
                        SpawnFood(Spawners[3], ref Food4, 4);
                    }
                    break;                    
                case 5:
                    if (Food5 == null)
                    {
                        SpawnFood(Spawners[4], ref Food5, 5);
                    }
                    break;
                default:
                    Debug.Log("ERROR NO FOOD FOUND!");
                    break;
            }
            CountDown = 6f;
        }
	}

    void SpawnFood(GameObject Spawner, ref GameObject FoodRef, int IdNum)
    {
        Vector3 spawnPosition = new Vector3(Spawner.transform.position.x, Spawner.transform.position.y, 0);

        FoodRef = Instantiate(FoodObject, spawnPosition, Quaternion.identity);
        FoodRef.GetComponent<S_Food>().SetIdNum(IdNum);
        
    }

    public void SetFoodToNull(int IdNum)
    {
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
