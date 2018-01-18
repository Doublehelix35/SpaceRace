﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PacMonster : MonoBehaviour {

    public Transform Waypoint01;
    public Transform Waypoint02;
    public GameObject Projectile;
    public Transform FirePoint;
    public Transform FirePoint02;

    public Vector3 Scale = new Vector3(2f,2f,2f);
    public float Speed = 1f;
    float PauseSpeed = 1f;
    bool FaceRight = true;

    Vector2 dir = Vector2.right;

    public float ShootInterval = 3f;
    float TimeAtLastShoot;
    bool ShootOnce = false;


    // Use this for initialization
    void Start () {
        TimeAtLastShoot = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

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
        
    }

    void Shoot()
    {
        if (FaceRight)
        {
            // Create projectile
            GameObject Bullet = Instantiate(Projectile, FirePoint.transform.position, transform.rotation); // Left
            GameObject Bullet2 = Instantiate(Projectile, FirePoint.transform.position, transform.rotation); // Middle
            GameObject Bullet3 = Instantiate(Projectile, FirePoint.transform.position, transform.rotation); // Right

            // Make bullets go 45 degrees apart
            Bullet.GetComponent<S_Bullet>().ChangeBulletDirection(new Vector3(1f, 0.5f, 0f));
            Bullet3.GetComponent<S_Bullet>().ChangeBulletDirection(new Vector3(1f, -0.5f, 0f));
        }
        else
        {
            // Create projectile
            GameObject Bullet = Instantiate(Projectile, FirePoint02.transform.position, transform.rotation); // Left
            GameObject Bullet2 = Instantiate(Projectile, FirePoint02.transform.position, transform.rotation); // Middle
            GameObject Bullet3 = Instantiate(Projectile, FirePoint02.transform.position, transform.rotation); // Right

            // Make bullets go 45 degrees apart
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
        
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationToShoot, Time.time * 2f);

        yield return new WaitForSeconds(0.5f);

        
        if (!ShootOnce)
        {
            Shoot();
            ShootOnce = true;
        }
        

        yield return new WaitForSeconds(0.5f);

        // Rotate to back
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
}
