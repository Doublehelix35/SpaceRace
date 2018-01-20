using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LineManager : MonoBehaviour {

    // Start, middle and end of the line
    public GameObject StartPos;
    public GameObject MiddlePos;
    public GameObject EndPos;
    LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();

        // Update line
        line.SetPosition(0, StartPos.transform.position);
        line.SetPosition(1, MiddlePos.transform.position);
        line.SetPosition(2, EndPos.transform.position);
    }

    void Update()
    {
        // Update line
        line.SetPosition(0, StartPos.transform.position);
        line.SetPosition(1, MiddlePos.transform.position);
        line.SetPosition(2, EndPos.transform.position);
    }
}
