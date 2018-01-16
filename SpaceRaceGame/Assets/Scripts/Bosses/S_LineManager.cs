using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LineManager : MonoBehaviour {

    public GameObject StartPos;
    public GameObject MiddlePos;
    public GameObject EndPos;
    LineRenderer line;

    // Use this for initialization
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, StartPos.transform.position);
        line.SetPosition(1, MiddlePos.transform.position);
        line.SetPosition(2, EndPos.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, StartPos.transform.position);
        line.SetPosition(1, MiddlePos.transform.position);
        line.SetPosition(2, EndPos.transform.position);
    }
}
