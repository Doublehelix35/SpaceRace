using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_PacNightmare : MonoBehaviour {

    public float TimeBetweenShotsMin = 1f; // Min
    public float TimeBetweenShotsMax = 3f; // Max
    float TimeBetweenShots; // Time until next shot
    float LastShotTime; // Time.time at last shot
    float RotationSpeed = 1f;
    int BulletPhase = 3; // Determines how many bullets get fired
    public float BulletSpeed = 4f; // 4 is normal bullet speed
    public int BaseHealth = 10; // Max health
    int Health = 10; // Current health

    public GameObject Projectile; // Projectile prefab
    public Transform FirePoint;
    GameObject PlayerRef;
    GameObject GameManagerRef;


    void Start ()
    {
        // Initialize values
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        GameManagerRef = GameObject.Find("GameManager");
        LastShotTime = Time.time;
        TimeBetweenShots = Random.Range(TimeBetweenShotsMin, TimeBetweenShotsMax);
        Health = BaseHealth;
    }
	
	void Update ()
    {
        // Direction of player
        Vector3 TargetDir = PlayerRef.transform.position - transform.position;
        // Calc angle
        float angle = Mathf.Atan2(TargetDir.y, TargetDir.x) * Mathf.Rad2Deg;
        // Rotate gameobject by angle around an axis
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        if(LastShotTime + TimeBetweenShots < Time.time)
        {
            Shoot();

            // Get new time between shots
            TimeBetweenShots = Random.Range(TimeBetweenShotsMin, TimeBetweenShotsMax);
            // Reset last shot time
            LastShotTime = Time.time;
        }

        // Manage phase
        PhaseManager();
    }

    void Shoot()
    {
        switch (BulletPhase)
        {
            case 1:

                break;
            case 2:
                break;
            case 3:
                // Create projectile
                GameObject Bullet = Instantiate(Projectile, FirePoint.transform.position, transform.rotation); // Left
                GameObject Bullet2 = Instantiate(Projectile, FirePoint.transform.position, transform.rotation); // Middle
                GameObject Bullet3 = Instantiate(Projectile, FirePoint.transform.position, transform.rotation); // Right

                // Spawn bullets 45 degrees apart
                Bullet.GetComponent<S_Bullet>().ChangeBulletDirection(new Vector3(1f, 0.5f, 0f));
                Bullet3.GetComponent<S_Bullet>().ChangeBulletDirection(new Vector3(1f, -0.5f, 0f));

                // Slow bullets
                Bullet.GetComponent<S_Bullet>().SetBulletSpeed(BulletSpeed, 5f);
                Bullet2.GetComponent<S_Bullet>().SetBulletSpeed(BulletSpeed, 5f);
                Bullet3.GetComponent<S_Bullet>().SetBulletSpeed(BulletSpeed, 5f);
                break;
            case 4:
                break;
            case 5:
                break;
            default:
                break;
        }
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
                SceneManager.LoadScene("Win");
            }
        }
    }
}
