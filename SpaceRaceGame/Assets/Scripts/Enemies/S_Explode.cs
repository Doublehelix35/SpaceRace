using UnityEngine;
using System.Collections;

public class S_Explode : MonoBehaviour {

    public int Health = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Bullet")
        {
            // explode sprite change
            Health--;

            if(Health <= 0)
            {
                Destroy(gameObject, 0f);
            }            
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Bullet")
        {
            // explode sprite change
            Health--;

            if (Health <= 0)
            {
                Destroy(gameObject, 0f);
            }
        }
    }
}
