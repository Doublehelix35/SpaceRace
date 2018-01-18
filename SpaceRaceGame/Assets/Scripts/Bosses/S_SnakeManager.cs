using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S_SnakeManager : MonoBehaviour {

    public GameObject[] BossBars; // boss bar ui images
    //public GameObject Boss; // boss game object
    public Sprite[] BossBarsSprites; // make sure they are same number as boss bars & put them in back to front

    public GameObject[] Spawners;
    public GameObject[] Snakes;

    int SnakeSize = 1;
    int CurrentSpawner = 0;
    float MoveSpeed = 1.5f;
    //bool BossCanSpawn = true;
    //float RespawnTimer = 0.1f;

    int SnakeHealth = 100;
    int SnakeHealthBase = 100;

    

    void Start()
    {
        SpawnBoss();        
    }

    void Update()
    {
        int phase = 8;
        if (SnakeHealth <= ((SnakeHealthBase / 8) * 7)) // 7/8
        {
            phase = 7;
            MoveSpeed = 2f;
        }

        if (SnakeHealth <= ((SnakeHealthBase / 8) * 6)) // 6/8
        {
            phase = 6;
            MoveSpeed = 2.5f;
        }

        if (SnakeHealth <= ((SnakeHealthBase / 8) * 5)) // 5/8
        {
            phase = 5;
            MoveSpeed = 3f;
        }

        if (SnakeHealth <= ((SnakeHealthBase / 8) * 4)) // 4/8
        {
            phase = 4;
            MoveSpeed = 3.5f;
        }

        if (SnakeHealth <= ((SnakeHealthBase / 8) * 3)) // 3/8
        {
            phase = 3;
            MoveSpeed = 4f;
        }

        if (SnakeHealth <= ((SnakeHealthBase / 8) * 2)) // 2/8
        {
            phase = 2;
            MoveSpeed = 4.5f;
        }

        if (SnakeHealth <= (SnakeHealthBase / 8)) // 1/8
        {
            phase = 1;
            MoveSpeed = 5f;
        }

        if (SnakeHealth < 1) // 0/8
        {
            phase = 0;
        }

        PhaseUpdater(phase); // count down from 7 (There are 8 phases from 7 to 0)
    }

    public void PhaseUpdater(int PhaseNum)
    {

        for (int i = 0; i < BossBars.Length; i++)
        {
            if (i == PhaseNum)
            {
                BossBars[i].GetComponent<Image>().sprite = BossBarsSprites[i];
            }
        }
    }

    void SpawnBoss()
    {
        //if (!BossCanSpawn)
        //{
           // return;
        //}

        Debug.Log("Current spawner: " + CurrentSpawner + " Spawner length: " + Spawners.Length);
        //Debug.Log("BOSS SPAWNED");
        if (CurrentSpawner == 0)
        {
            Vector3 spawnPosition = new Vector3(Spawners[CurrentSpawner].transform.position.x, Spawners[CurrentSpawner].transform.position.y, 0);
            GameObject Snake = Instantiate(Snakes[SnakeSize - 1], spawnPosition, Quaternion.identity);

            Snake.GetComponent<Rigidbody2D>().velocity = Vector2.right * MoveSpeed * Time.deltaTime * 20f;
            CurrentSpawner++;

            //BossCanSpawn = false;
        }
        else if (CurrentSpawner % 2 == 0) // even numbers
        {
            Vector3 spawnPosition = new Vector3(Spawners[CurrentSpawner].transform.position.x, Spawners[CurrentSpawner].transform.position.y, 0);
            GameObject Snake = Instantiate(Snakes[SnakeSize - 1], spawnPosition, Quaternion.identity);

            Snake.GetComponent<Rigidbody2D>().velocity = Vector2.right * MoveSpeed * Time.deltaTime * 20f;

            CurrentSpawner++;
            if(CurrentSpawner >= Spawners.Length)
            {
                CurrentSpawner = 0;
                
            }
            //BossCanSpawn = false;
        }
        else // odd numbers
        {
            Vector3 spawnPosition = new Vector3(Spawners[CurrentSpawner].transform.position.x, Spawners[CurrentSpawner].transform.position.y, 0);
            GameObject Snake = Instantiate(Snakes[SnakeSize - 1], spawnPosition, Quaternion.identity);

            Snake.GetComponent<Rigidbody2D>().velocity = Vector2.left * MoveSpeed * Time.deltaTime * 20f;
            Snake.transform.rotation = Quaternion.Euler(0, 180, 0);
                        
            CurrentSpawner++;
            if (CurrentSpawner >= Spawners.Length)
            {
                CurrentSpawner = 0;
                
            }
            //BossCanSpawn = false;
        }
        
        
    }

    public void IncreaseSnakeSize()
    {
        SnakeSize++;

        if(SnakeSize > Snakes.Length)
        {
            SnakeSize = Snakes.Length;
        }
    }

    public void NextSpawn()
    {
        SpawnBoss();
    }

    public void LoseHealth()
    {
        SnakeHealth--;

        if(SnakeHealth <= 0)
        {
            SceneManager.LoadScene("Win");
        }
    }
}
