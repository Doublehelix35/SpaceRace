using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PacMonster : MonoBehaviour {

    public Transform Waypoint01;
    public Transform Waypoint02;

    public Vector3 Scale = new Vector3(2f,2f,2f);
    public float Speed = 1f;
    bool FaceRight = true;

    Vector2 dir = Vector2.right;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        

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

        if (FaceRight)
        {
            transform.localScale = new Vector3(Scale.x, Scale.y, Scale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Scale.x, Scale.y, Scale.z);
        }

        transform.Translate(dir * Speed * Time.deltaTime);
    }
}
