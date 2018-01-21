using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SnakeBoss : MonoBehaviour {

    GameObject SnakeManagerRef;
    Transform FirePoint;

    public bool SurvivedOnce = false; // Survives kill box once

    public GameObject Projectile; // Projectile prefab
    float ShootWaitTime = 1f;
    float lastShootTime = 0f;

    bool ShootLeft = false; // Direction to shoot

    void Start ()
    {
        // Initialize values
        SnakeManagerRef = GameObject.Find("SnakeManager");

        // Find fire point amongst child objects
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "FirePoint")
            {
                FirePoint = gameObject.transform.GetChild(i);
            }
            Debug.Log("Child: " + i);       
        }

        lastShootTime = Time.time;
    }

	void Update ()
    {
        // Check if can shoot
        if ((Time.time - lastShootTime) >= ShootWaitTime)
        {
            Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            SnakeManagerRef.GetComponent<S_SnakeManager>().LoseHealth();
        }        
    }

    void Shoot()
    {

        // Create projectile
        GameObject Bullet = Instantiate(Projectile, FirePoint.transform.position, transform.rotation);
        GameObject Bullet2 = Instantiate(Projectile, FirePoint.transform.position, transform.rotation);

        // Make bullets go diagonally 90 degrees apart
        Bullet.GetComponent<S_Bullet>().ChangeBulletDirection(new Vector3(1f, 0.5f, 0f));
        Bullet2.GetComponent<S_Bullet>().ChangeBulletDirection(new Vector3(1f, -0.5f, 0f));


        // Update last shoot time
        lastShootTime = Time.time;

    }
    
}
