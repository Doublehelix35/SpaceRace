using UnityEngine;
using System.Collections;

public class S_Camera : MonoBehaviour {

    private GameObject PlayerRef;
   
    // Use this for initialization
    void Start ()
    {

        PlayerRef = GameObject.FindGameObjectWithTag("Player");

        gameObject.transform.position = new Vector3(PlayerRef.transform.position.x, PlayerRef.transform.position.y, -100);

	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position = new Vector3(PlayerRef.transform.position.x, PlayerRef.transform.position.y, -100);
    }
}
