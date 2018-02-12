using UnityEngine;
using System.Collections;

public class S_Camera : MonoBehaviour {

    GameObject PlayerRef;
    public float OffsetY = 0;
    public float OffsetX = 0;

    void Start ()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
	}

	void Update ()
    {
        // Set gameobject pos equal to player's pos
        gameObject.transform.position = new Vector3(PlayerRef.transform.position.x + OffsetX, PlayerRef.transform.position.y + OffsetY, -100);
    }
}
