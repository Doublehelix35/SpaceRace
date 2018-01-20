using UnityEngine;
using System.Collections;

public class S_AI_Homing : MonoBehaviour {

    private GameObject PlayerRef;

    public float speed = 0.2f; // Speed of movement
    public float rotationSpeed = 1f;
    public float ActivationDistance = 2f;

	void Start ()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
	}

	void Update ()
    {
        // Calculate distance
        float Distance = Vector3.Distance(PlayerRef.transform.position, transform.position); // calculate distance

        // Check Distance
        if (Distance < ActivationDistance)
        {
            // Direction to head in
            Vector3 dir = PlayerRef.transform.position - transform.position;
            // Only needed if objects don't share 'z' value.
            dir.z = 0.0f;
            if (dir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.left, dir), rotationSpeed * Time.deltaTime);
            }

            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, PlayerRef.transform.position, speed * Time.deltaTime); 

        }

    }
}
