using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_AsteroidBoss : MonoBehaviour {

    public int BaseHealth = 10;
    int Health = 10;

    private GameObject GameManagerRef;

    // Use this for initialization
    void Start () {
        GameManagerRef = GameObject.Find("GameManager");

        Health = BaseHealth;
    }
	
	// Update is called once per frame
	void Update () {

        int phase = 8;
        if (Health <= ((BaseHealth / 8) * 7))
        {
            phase = 7;
        }

        if (Health <= ((BaseHealth / 8) * 6))
        {
            phase = 6;
        }

        if (Health <= ((BaseHealth / 8) * 5))
        {
            phase = 5;
        }

        if (Health <= ((BaseHealth / 8) * 4))
        {
            phase = 4;
        }

        if (Health <= ((BaseHealth / 8) * 3))
        {
            phase = 3;
        }

        if (Health <= ((BaseHealth / 8) * 2))
        {
            phase = 2;
        }

        if (Health <= (BaseHealth / 8))
        {
            phase = 1;
        }

        if (Health <= 1)
        {
            phase = 0;
        }

        GameManagerRef.GetComponent<S_PhaseManager>().PhaseUpdater(phase); // count down from 7 (There are 8 phases from 7 to 0)
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            //Lose health
            Health--;
            if (Health <= 0)
            {
                SceneManager.LoadScene("Win");
            }
        }

    }
}
