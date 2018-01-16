using UnityEngine;
using System.Collections;

public class S_AI_Homing : MonoBehaviour {

    private GameObject PlayerRef;

    public float speed = 0.2f;
    public float rotationSpeed = 1f;
    public float ActivationDistance = 2f;

	// Use this for initialization
	void Start () {
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        float Distance = Vector3.Distance(PlayerRef.gameObject.transform.position, this.transform.position); // calculate distance

        if (Distance < ActivationDistance)
        {
            Vector3 dir = PlayerRef.transform.position - transform.position;
            // Only needed if objects don't share 'z' value.
            dir.z = 0.0f;
            if (dir != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.left, dir), rotationSpeed * Time.deltaTime);

            this.transform.position = Vector3.MoveTowards(this.transform.position, PlayerRef.gameObject.transform.position, speed * Time.deltaTime); // move towards the player

            // Debug.Log("ACTIVE" + Distance);
        }

    }
}
