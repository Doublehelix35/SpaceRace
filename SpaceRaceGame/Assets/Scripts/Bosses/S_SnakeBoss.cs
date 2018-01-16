using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SnakeBoss : MonoBehaviour {

    GameObject SnakeManagerRef;
    Transform FirePoint;

    public bool SurvivedOnce = false;

    public GameObject Projectile;
    float ShootWaitTime = 1f;
    float currentTime = 0f;
    float lastShootTime = 0f;

    bool ShootLeft = false;

    // Use this for initialization
    void Start () {

        SnakeManagerRef = GameObject.Find("SnakeManager");

        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "FirePoint")
            {
                FirePoint = gameObject.transform.GetChild(i);
            }
            Debug.Log("Child: " + i);       
        }
        
        currentTime = Time.time;
        lastShootTime = Time.time;

        Debug.Log(gameObject.transform.childCount);
    }
	
	// Update is called once per frame
	void Update () {

        // Update current time
        currentTime = Time.time;

        // If its been longer than wait time then call shoot function
        if ((currentTime - lastShootTime) >= ShootWaitTime)
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
        GameObject Bullet = Instantiate(Projectile, FirePoint.transform.position, this.transform.rotation);
        GameObject Bullet2 = Instantiate(Projectile, FirePoint.transform.position, this.transform.rotation);
        
        // Make bullets go diagonally 90 degrees apart
        Bullet.GetComponent<S_Bullet>().ChangeBulletDirection(new Vector3(1f, 0.5f, 0f));
        Bullet2.GetComponent<S_Bullet>().ChangeBulletDirection(new Vector3(1f, -0.5f, 0f));


        // Update last shoot time
        lastShootTime = Time.time;

    }
    
}
