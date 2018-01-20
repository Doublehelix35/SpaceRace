using UnityEngine;
using System.Collections;

public class S_PingPong : MonoBehaviour
{
    float distance = 4f;
    float offset = 2f;

    void Update()
    {
        // Move back and forth over time
        transform.position = new Vector3(Mathf.PingPong(Time.time, distance) - offset, transform.position.y, transform.position.z);
    }
}

