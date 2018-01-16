using UnityEngine;
using System.Collections;

public class S_ArcShooting : MonoBehaviour {

    public GameObject Projectile;
    public float ShootWaitTime = 6f;
    float currentTime = 0f;
    float lastShootTime = 0f;

    public float rotateMin = -30f;
    public float rotateMax = 30f;
    public float rotateStart = 270f;
    

    private Vector3 currentAngle;

    // Use this for initialization
    void Start () {

        currentTime = Time.time;
        lastShootTime = Time.time;

        currentAngle = transform.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {

        // Update current time
        currentTime = Time.time;

        // If its been longer than wait time then call shoot function
        if((currentTime - lastShootTime) >= ShootWaitTime)
        {
            Shoot();
        }


	}

    void Shoot()
    {
        // Rotate object
        currentAngle = new Vector3(0f, 0f, rotateStart + Random.Range(rotateMin, rotateMax));

        transform.eulerAngles = currentAngle;

        // Set firepoint position as a vector
        Vector3 firePointPosition = new Vector3(this.transform.position.x, this.transform.position.y, 0);

        // Create projectile
        Instantiate(Projectile, firePointPosition, this.transform.rotation);

        // Update last shoot time
        lastShootTime = Time.time;

    }

    public void UpdateShootWaitTime(int PhaseNum) // takes phase number and sets wait time equal to it + 1
    {
            ShootWaitTime = PhaseNum + 1;
        
    }
}
