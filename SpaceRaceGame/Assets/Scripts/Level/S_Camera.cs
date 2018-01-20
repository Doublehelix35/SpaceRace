using UnityEngine;
using System.Collections;

public class S_Camera : MonoBehaviour {

    GameObject PlayerRef;

    void Start ()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
	}

	void Update ()
    {
        // Set gameobject pos equal to player's pos
        gameObject.transform.position = new Vector3(PlayerRef.transform.position.x, PlayerRef.transform.position.y, -100);
    }
}
