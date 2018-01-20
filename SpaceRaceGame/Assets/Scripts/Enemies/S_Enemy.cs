using UnityEngine;
using System.Collections;

public class S_Enemy : MonoBehaviour {

    public bool Respawns = false;
    public bool IsExplodable = false;

    public int BaseHealth = 10; // Max health
    int Health = 10; // Current health

    float BaseRespawnTime = 15f; // Max respawn time
    float RespawnTime = 15f; // Current respawn time

    //Threat size affects physical size, health, loot min and max and respawn times
    public int ThreatSize = 1; // bigger the threat the bigger the rewards

    int NumOfLootMin = 1;
    int NumOfLootMax = 2;
    public int LootIncreaser = 0;
    int LootType = 0; // 0 = rock, 1 = metal, 2 = Crystal, 3 = Uranium

    // Resource prefabs
    public GameObject Rock;
    public GameObject Metal;
    public GameObject Crystal;
    public GameObject Uranium;

    GameObject GameManagerRef;

    void Start()
    {
        // Initialize values
        Health = BaseHealth;
        ThreatCalcs(Respawns);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            // Lose health
            Health--;
            if (Health <= 0) // Check health
            {
                DestroyAndSpawn(0);
            }
        }

        if (col.gameObject.tag == "Player")
        {
            if (IsExplodable)
            {            
                DestroyAndSpawn(0.1f);
            }
        }
    }
    
    void DestroyAndSpawn(float TimeToWait)
    {
        // Spawn loot
        SpawnLoot();
        
        // Then destroy self
        Destroy(gameObject, TimeToWait);
    }

    void SpawnLoot()
    {
        // Calculate num of loot
        int NumOfLoot = Random.Range(NumOfLootMin, NumOfLootMax + 1);
        NumOfLoot = NumOfLoot + LootIncreaser;

        // Spawn loot
        for (int i = 0; i < NumOfLoot; i++)
        {
            // Determine loot type
            LootType = Random.Range(0, 4);

            switch (LootType)
            {
                case 0:
                    // Spawn rock
                    Instantiate(Rock, transform.position, Quaternion.identity);
                    break;
                case 1:
                    // Spawn Metal
                    Instantiate(Metal, transform.position, Quaternion.identity);
                    break;
                case 2:
                    // Spawn Crystal
                    Instantiate(Crystal, transform.position, Quaternion.identity);
                    break;
                case 3:
                    // Spawn Uranium
                    Instantiate(Uranium, transform.position, Quaternion.identity);
                    break;
                default:
                    // ERROR!!!
                    Debug.Log("No Loot spawned! I am " + gameObject.name);
                    break;
            }
        }
    }

    private void ThreatCalcs(bool WillRespawn)
    {
        // Size
        if (ThreatSize <= 0) // Dont go below or equal to 0 scale
        {
            gameObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0);
        }
        else // Set scale equal to threat size
        {
            gameObject.transform.localScale = new Vector3(transform.localScale.x * ThreatSize, transform.localScale.y * ThreatSize, 0);
        }
       

        // Health
               
        float HealthCalc = BaseHealth * ThreatSize * 0.75f; // Calcualte health

        if (ThreatSize <= 0)
        {
            HealthCalc = BaseHealth; // Make sure base health is minimum
        }

        Health = (int)HealthCalc; // convert back to int

        // Loot Min
        int reduction = ThreatSize - 1; // how much to reduce by

        if(reduction <= 0) // Make sure reduction doesnt go below 0
        {
            reduction = 0;
        }

        NumOfLootMin = ThreatSize - reduction; // Calculate min loot

        // Loot Max
        NumOfLootMax = ThreatSize;

        // Respawn time
        if (WillRespawn)
        {
            RespawnTime = BaseRespawnTime * ThreatSize;
        }
    }


}
