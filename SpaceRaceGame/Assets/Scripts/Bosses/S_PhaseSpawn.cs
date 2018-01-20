using UnityEngine;
using System.Collections;

public class S_PhaseSpawn : MonoBehaviour {

    // Object prefabs
    public GameObject Object01; 
    public GameObject Object02;

    public float distanceLeft = -5f; // Min
    public float distanceRight = 5f; // Max

    public float SpawnWaitTime = 3f; // Time until spawn
    float lastSpawnTime = 0f; // Time.time at last spawn

    int PhaseNum = 8; // Current phase

    void Start ()
    {
        lastSpawnTime = Time.time;
    }

	void Update ()
    {
        // Check if can spawn
        if ((Time.time - lastSpawnTime) >= SpawnWaitTime)
        {
            // Spawn based on phase num
            switch (PhaseNum)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    int randNum = Random.Range(1, 3);
                    if (randNum == 1)
                    {
                        SpawnObject(Object01);
                    }
                    else
                    {
                        SpawnObject(Object02);
                    }
                    break;
                                       
                case 5:
                case 6:
                    SpawnObject(Object01);
                    break;

                case 7:
                case 8:
                    SpawnObject(Object02);
                    break;

                default:
                    break;
            }
        }
	}

    void SpawnObject(GameObject objectToSpawn)
    {
        // Distance to spawn at
        float RandDistance = Random.Range(distanceLeft, distanceRight);

        // Position to spawn at
        Vector3 spawnPosition = new Vector3(transform.position.x + RandDistance, transform.position.y, 0);

        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        lastSpawnTime = Time.time; // Update timer
    }

    public void SetPhaseNum(int CurPhaseNum)
    {
        // Update phase num
        PhaseNum = CurPhaseNum; 

        // Scale spawn time based on phase number
        switch (PhaseNum) 
        {
            case 0:
            case 1:
            case 2:
                SpawnWaitTime = 1.5f;
                break;
            
            case 3:
            case 4:
                SpawnWaitTime = 2f;
                break;
            
            case 5:
            case 6:
                SpawnWaitTime = 3;
                break;
            
            case 7:
            case 8:
                SpawnWaitTime = 4;
                break;

            default:
                break;
        }
    }
}
