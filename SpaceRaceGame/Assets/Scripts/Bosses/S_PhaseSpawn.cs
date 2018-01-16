using UnityEngine;
using System.Collections;

public class S_PhaseSpawn : MonoBehaviour {

    public GameObject Object01;
    public GameObject Object02;

    public float distanceLeft = -5f;
    public float distanceRight = 5f;

    public float SpawnWaitTime = 3f;
    float currentTime = 0f;
    float lastShootTime = 0f;

    private int PhaseNum = 8;

    // Use this for initialization
    void Start () {

        currentTime = Time.time;
        lastShootTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {

        currentTime = Time.time;

        // If its been longer than shoot wait time then call shoot function
        if ((currentTime - lastShootTime) >= SpawnWaitTime)
        {
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
        float RandDistance = Random.Range(distanceLeft, distanceRight);

        Vector3 spawnPosition = new Vector3(this.transform.position.x + RandDistance, this.transform.position.y, 0);

        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        lastShootTime = Time.time;
    }

    public void SetPhaseNum(int CurPhaseNum)
    {
        PhaseNum = CurPhaseNum;

        switch (PhaseNum) // scale spawn time based on phase number
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
