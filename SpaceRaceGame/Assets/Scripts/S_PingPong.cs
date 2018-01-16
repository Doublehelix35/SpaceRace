using UnityEngine;
using System.Collections;

public class S_PingPong : MonoBehaviour
{

    private float distance = 4f;
    private float offset = 2f;

    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time, distance) - offset, transform.position.y, transform.position.z);
    }
}



/*
public Transform startMarker;
public Transform endMarker;
public float speed = 1.0f;
private float startTime;
private float journeyLength;
private bool isHeadingToEnd = true;
private bool onlyOnce = false;


void Start()
{
    startTime = Time.time;
    journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
}
void Update()
{
    float distCovered = (Time.time - startTime) * speed;
    float fracJourney = distCovered / journeyLength;

    if(this.transform.position.x == endMarker.position.x)
    {

        SwitchDirection(false);

    }
    else if (this.transform.position.x == startMarker.position.x)
    {
        SwitchDirection(true);
    }


    if (isHeadingToEnd)
    {
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
    }
    else if(!isHeadingToEnd)
    {
        transform.position = Vector3.Lerp(endMarker.position, startMarker.position, fracJourney);
    }

}

void SwitchDirection(bool GoToEnd)
{


    startTime = Time.time;
    journeyLength = Vector3.Distance(startMarker.position, endMarker.position);


    if (GoToEnd)
    {
        isHeadingToEnd = true;
    }
    else
    {
        isHeadingToEnd = false;
    }
}
}*/

