using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class S_BackgroundManager : MonoBehaviour {

    [SerializeField]S_BackgroundScrolling[] Backgrounds;

    Vector2 LastFramePos;

	// Use this for initialization
	void Start () {
        LastFramePos = new Vector2(this.transform.position.x, this.transform.position.y); // Starting position
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 CurPos = new Vector2(this.transform.position.x, this.transform.position.y); // Current position

        if(LastFramePos == CurPos) // if havent moved, exit
        {
            return;
        }

        Vector2 pos;
        pos = LastFramePos - CurPos;

        pos.x = pos.x - (int)pos.x;
        pos.y = pos.y - (int)pos.y;

        foreach(S_BackgroundScrolling s in Backgrounds)
        {
            s.MoveTexture(pos);
        }

        LastFramePos = CurPos;
	}
}
