using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_MainMenu : MonoBehaviour {

    public Text Title;
    float SizeToScale = 0.2f;

	
	void Start ()
    {

	}
	
	void Update ()
    {
        Vector3 ScaledVec = new Vector3(Mathf.PingPong(Time.time / 6, SizeToScale) + 1f, 
                                        Mathf.PingPong(Time.time / 6, SizeToScale) + 1f, 
                                        0f);
        Title.rectTransform.localScale = ScaledVec;

    }
}
