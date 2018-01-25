using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_PacMonster : MonoBehaviour {

    // Pathfinding waypoints
    public Transform Waypoint01;
    public Transform Waypoint02;

    public GameObject Projectile; // Projectile prefab
    public Transform FirePoint; 
    public Transform FirePoint02;
    GameObject GameManagerRef;

    public float Speed = 1f; // Speed of boss
    float PauseSpeed = 1f; // Used to pause movement
    bool FaceRight = true;

    Vector2 dir = Vector2.right; // Direction of movement

    public float ShootInterval = 3f; // Time between shots
    float TimeAtLastShoot; // Time.time at last shot
    bool ShootOnce = false; 

    public int BaseHealth = 10; // Max health
    int Health = 10; // Current health


    void Start ()
    {
        // Initialize values
        TimeAtLastShoot = Time.time;
        Health = BaseHealth;
        GameManagerRef = GameObject.Find("GameManager");
    }
	

	void Update ()
    {
        // Check position agaisnt waypoints and adjust direction
		if(transform.position.x < Waypoint01.position.x)
        {
            dir = Vector2.right;
            FaceRight = true;
        }
        else if (transform.position.x > Waypoint02.position.x)
        {
            dir = Vector2.left;
            FaceRight = false;
        }

        // Make the gameobject face left or right
        if (FaceRight)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        // Move or shoot
        if (TimeAtLastShoot + ShootInterval < Time.time)
        {
            StartCoroutine("WaitShootWait");
        }
        else 
        {
            // Move
            transform.Translate(dir * Speed * PauseSpeed * Time.deltaTime);
        }

        // Manage phase
        PhaseManager();

    }

    void Shoot()
    {
        if (FaceRight) // Facing right
        {
            // Create projectile
            GameObject Bullet = Instantiate(Projectile, FirePoint.transform.position, transform.rotation); // Left
            GameObject Bullet2 = Instantiate(Projectile, FirePoint.transform.position, transform.rotation); // Middle
            GameObject Bullet3 = Instantiate(Projectile, FirePoint.transform.position, transform.rotation); // Right

            // Spawn bullets 45 degrees apart
            Bullet.GetComponent<S_Bullet>().ChangeBulletDirection(new Vector3(1f, 0.5f, 0f));
            Bullet3.GetComponent<S_Bullet>().ChangeBulletDirection(new Vector3(1f, -0.5f, 0f));
        }
        else // Facing left
        {
            // Create projectile
            GameObject Bullet = Instantiate(Projectile, FirePoint02.transform.position, transform.rotation); // Left
            GameObject Bullet2 = Instantiate(Projectile, FirePoint02.transform.position, transform.rotation); // Middle
            GameObject Bullet3 = Instantiate(Projectile, FirePoint02.transform.position, transform.rotation); // Right

            // Spawn bullets 45 degrees apart
            Bullet.GetComponent<S_Bullet>().ChangeBulletDirection(new Vector3(-1f, 0.5f, 0f));
            Bullet2.GetComponent<S_Bullet>().ChangeBulletDirection(new Vector3(-1f, 0f, 0f));
            Bullet3.GetComponent<S_Bullet>().ChangeBulletDirection(new Vector3(-1f, -0.5f, 0f));
        }        
    }

    IEnumerator WaitShootWait()
    {
        // Stop moving
        PauseSpeed = 0;

        // Rotate to shoot
        Quaternion rotationToShoot;

        if (FaceRight)
        {
            rotationToShoot = Quaternion.Euler(new Vector3(0, 0, 270));
        }
        else
        {
            rotationToShoot = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        
        // Rotate to face down
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationToShoot, Time.time * 2f);

        yield return new WaitForSeconds(0.5f);

        if (!ShootOnce) // Check if already shot
        {
            Shoot();
            ShootOnce = true;
        }
        
        yield return new WaitForSeconds(0.5f);

        // Rotate back
        Quaternion rotationBack = Quaternion.Euler(new Vector3(0, 0, 0));
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationBack, Time.time * 2f);

        // Start moving
        PauseSpeed = 1f;

        // Reset timer
        TimeAtLastShoot = Time.time;

        yield return new WaitForSeconds(0.5f);

        // Reset ShootOnce
        ShootOnce = false;
    }

    void PhaseManager()
    {
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            //Lose health
            Health--;
            if (Health <= 0) // Check health
            {
                SceneManager.LoadScene("BossPacNightmare");
            }
        }
    }
}
