using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_AsteroidBoss : MonoBehaviour {

    public int BaseHealth = 10; // Max health
    int Health = 10; // Current health

    private GameObject GameManagerRef;

    void Start ()
    {
        // Initialize values
        GameManagerRef = GameObject.Find("GameManager");
        Health = BaseHealth;
    }

	void Update ()
    {
        // Set phase based on health
        int phase = 8; // 8/8
        if (Health <= ((BaseHealth / 8) * 7)) // 7/8
        {
            phase = 7;
        }

        if (Health <= ((BaseHealth / 8) * 6)) // 6/8
        {
            phase = 6;
        }

        if (Health <= ((BaseHealth / 8) * 5)) // 5/8
        {
            phase = 5;
        }

        if (Health <= ((BaseHealth / 8) * 4)) // 4/8
        {
            phase = 4;
        }

        if (Health <= ((BaseHealth / 8) * 3)) // 3/8
        {
            phase = 3;
        }

        if (Health <= ((BaseHealth / 8) * 2)) // 2/8
        {
            phase = 2;
        }

        if (Health <= (BaseHealth / 8)) // 1/8
        {
            phase = 1;
        }

        if (Health < 1) // 0/8
        {
            phase = 0;
        }

        // Tell game manager what phase the boss is on
        GameManagerRef.GetComponent<S_PhaseManager>().PhaseUpdater(phase); 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            //Lose health
            Health--;

            // Check health
            if (Health <= 0) 
            {
                SceneManager.LoadScene("Win");
            }
        }

    }
}
