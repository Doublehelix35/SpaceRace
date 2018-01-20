﻿using UnityEngine;
using System.Collections;

public class S_Player : MonoBehaviour {

    public float SpeedFactor = 3.0f; // Speed modifier
    float Speed = 80f;
    float RotationSpeed = -1.5f;
    bool PauseMovement = false; // To stop movement

    public float fireRate = 1;
    float timeToFire = 0;
    public GameObject FirePoint01;
    public GameObject FirePoint02;
    public GameObject Bullet;


    private bool UpdatingUI = false;
    public bool IsInShop = false;

    // Particles
    public ParticleSystem GunParticle01;
    public ParticleSystem GunParticle02;
    
    void Start ()
    {
        // Update spawn point
        S_GameManager.gameManager.RespawnRefresh(this.gameObject.transform);
    }

    void Update ()
    {
        if (PauseMovement) // if player is paused dont check for movement or shooting
        {
            return;
        }
	
        /****   Rotate Player   ****/

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Rotate left
            transform.Rotate(Vector3.back * RotationSpeed);
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Rotate right
            transform.Rotate(Vector3.forward * RotationSpeed);
        }


        /****   Move Player   ****/

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // Move forward
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * Speed * SpeedFactor * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            // Move backwards
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * -Speed * 0.5f * SpeedFactor * Time.deltaTime);
        }


        /****   Shoot   ****/

        if (Input.GetKey(KeyCode.Space) && Time.time > timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            Shoot();
        }

	}

    void Shoot()
    {
        Instantiate(Bullet, FirePoint01.transform.position, FirePoint01.transform.rotation); // bullet 01
        Instantiate(Bullet, FirePoint02.transform.position, FirePoint02.transform.rotation); // bullet 02

        GunParticle01.Play();
        GunParticle02.Play();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Resource")
        {
            Debug.Log("Resource triggered!");
            // Add resource
            int resourceTypeRef = col.gameObject.GetComponent<S_Resource>().ResourceType;

            // Update ui
            UpdatingUI = true;
            S_GameManager.gameManager.UpdateResourceAdd(resourceTypeRef);
            UpdatingUI = false;
            // Destroy resoure object
            Destroy(col.gameObject, 0f);
        }

        if (col.gameObject.tag == "Planet")
        {
            IsInShop = true;
        }

        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Boss")
        {
            S_GameManager.gameManager.HealthMinus();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Planet")
        {
            IsInShop = false;
        }
    }

    public bool GetUpdatingUIBool()
    {
        return UpdatingUI;
    }

    public void ActivatePauseMovement()
    {
        PauseMovement = true;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public void DeactivatePauseMovement()
    {
       PauseMovement = false;
       gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    

}
