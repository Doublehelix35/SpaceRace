using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S_SnakeManager : MonoBehaviour {

    public GameObject[] BossBars; // boss bar ui images
    public Sprite[] BossBarsSprites; // make sure they are same number as boss bars & put them in back to front

    public GameObject[] Spawners; // Array of spawners
    public GameObject[] Snakes; // Array of snake sizes

    int SnakeSize = 1; // Current snake size
    int CurrentSpawner = 0;
    float MoveSpeed = 1.5f; // Snake speed

    int SnakeHealth = 100; // Current health
    public int SnakeHealthBase = 100; // Max health

    void Start()
    {
        SnakeHealth = SnakeHealthBase;
        SpawnBoss();     
    }

    void Update()
    {
        // Set phase based on health
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

        PhaseUpdater(phase);
    }

    public void PhaseUpdater(int PhaseNum)
    {
        // Update boss bars based on phase
        for (int i = 0; i < BossBars.Length; i++)
        {
            if (i == PhaseNum)
            {
                BossBars[i].GetComponent<Image>().sprite = BossBarsSprites[i];
            }
        }
    }

    public void SpawnBoss()
    {
        Debug.Log("Current spawner: " + CurrentSpawner + " Spawner length: " + Spawners.Length);

        // Spawn boss at current spawner
        if (CurrentSpawner == 0)
        {
            // Set spawn position based on current spawner
            Vector3 spawnPosition = new Vector3(Spawners[CurrentSpawner].transform.position.x, Spawners[CurrentSpawner].transform.position.y, 0);
            // Spawn boss
            GameObject Snake = Instantiate(Snakes[SnakeSize - 1], spawnPosition, Quaternion.identity);

            // Set velocity
            Snake.GetComponent<Rigidbody2D>().velocity = Vector2.right * MoveSpeed * Time.deltaTime * 20f;

            // Update current spawner
            CurrentSpawner++;
        }
        else if (CurrentSpawner % 2 == 0) // even numbers
        {
            // Set spawn position based on current spawner
            Vector3 spawnPosition = new Vector3(Spawners[CurrentSpawner].transform.position.x, Spawners[CurrentSpawner].transform.position.y, 0);
            // Spawn boss
            GameObject Snake = Instantiate(Snakes[SnakeSize - 1], spawnPosition, Quaternion.identity);

            // Set velocity
            Snake.GetComponent<Rigidbody2D>().velocity = Vector2.right * MoveSpeed * Time.deltaTime * 20f;

            // Update current spawner
            CurrentSpawner++;
            if(CurrentSpawner >= Spawners.Length)
            {
                CurrentSpawner = 0;
            }
        }
        else // odd numbers
        {
            // Set spawn position based on current spawner
            Vector3 spawnPosition = new Vector3(Spawners[CurrentSpawner].transform.position.x, Spawners[CurrentSpawner].transform.position.y, 0);
            GameObject Snake = Instantiate(Snakes[SnakeSize - 1], spawnPosition, Quaternion.identity);

            // Set velocity
            Snake.GetComponent<Rigidbody2D>().velocity = Vector2.left * MoveSpeed * Time.deltaTime * 20f;

            // Rotate snake
            Snake.transform.rotation = Quaternion.Euler(0, 180, 0);

            // Update current spawner   
            CurrentSpawner++;
            if (CurrentSpawner >= Spawners.Length)
            {
                CurrentSpawner = 0;
                
            }
        }        
    }

    public void IncreaseSnakeSize()
    {
        SnakeSize++;

        if(SnakeSize > Snakes.Length) // Check snake size doesnt get bigger than is possible
        {
            SnakeSize = Snakes.Length;
        }
    }

    public void LoseHealth()
    {
        SnakeHealth--;

        if(SnakeHealth <= 0) // Check health
        {
            SceneManager.LoadScene("Win");
        }
    }
}
