using UnityEngine;
using System.Collections;

public class S_ArcShooting : MonoBehaviour {

    public GameObject Projectile;
    public float ShootWaitTime = 6f; // Time to wait before shooting
    float lastShootTime = 0f; // Time.time of last shot

    public float rotateMin = -30f; // Min rotation
    public float rotateMax = 30f; // Max rotation
    public float rotateStart = 270f; // Start rotation
    private Vector3 currentAngle; // Current angle of game object


    void Start ()
    {
        // Initialize values
        lastShootTime = Time.time;
        currentAngle = transform.eulerAngles;
    }

	void Update ()
    {
        // If its been longer than wait time then call shoot function
        if((Time.time - lastShootTime) >= ShootWaitTime)
        {
            Shoot();
        }
	}

    void Shoot()
    {
        // Rotate object
        currentAngle = new Vector3(0f, 0f, rotateStart + Random.Range(rotateMin, rotateMax)); // Set current angle on z axis
        transform.eulerAngles = currentAngle; // Set rotation equal to current angle

        // Set firepoint position as a vector
        Vector3 firePointPosition = new Vector3(transform.position.x, transform.position.y, 0f);

        // Create projectile
        Instantiate(Projectile, firePointPosition, transform.rotation);

        // Update last shoot time
        lastShootTime = Time.time;
    }

    public void UpdateShootWaitTime(int PhaseNum) 
    {
        // takes phase number and sets wait time equal to it + 1
        ShootWaitTime = PhaseNum + 1;
    }
}
