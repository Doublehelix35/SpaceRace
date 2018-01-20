using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Follow : MonoBehaviour {

    public Transform TransformToFollow;
    public float DistanceToFollowAt;

	void Update ()
    {
        // Set position equal to transform to follow's position + set distance
        transform.position = new Vector3(TransformToFollow.position.x + DistanceToFollowAt, TransformToFollow.position.y, transform.position.z);
	}
}
