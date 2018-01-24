using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MoveInDirection : MonoBehaviour {

    public Vector2 DirectionToHead;
    public float Speed = 1f;

	void Update ()
    {
        // Move in direction
        transform.Translate(DirectionToHead * Speed * Time.deltaTime);
	}
}
