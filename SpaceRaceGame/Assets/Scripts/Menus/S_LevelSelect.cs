using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_LevelSelect : MonoBehaviour {

    public Image AsteroidImage;
    public Image SnakeImage;
    public Image PacMonsterImage;
    public Text Title;
    float SizeToScale = 0.2f;

    public Sprite[] AsteroidSprites;
    public Sprite[] SnakeSprites;
    public Sprite[] PacMonsterSprites;

    float ImageRefreshTimer = 2f;
    float LastImageRefresh; // Time.time of last refresh
    int ImageCount = 0; // What image to show

    void Start ()
    {
        LastImageRefresh = Time.time;	
	}
	
	void Update ()
    {
        Vector3 ScaledVec = new Vector3(Mathf.PingPong(Time.time / 6, SizeToScale) + 1f,
                                        Mathf.PingPong(Time.time / 6, SizeToScale) + 1f,
                                        0f);
        Title.rectTransform.localScale = ScaledVec;

        if(LastImageRefresh + 2f < Time.time)
        {
            // Load new sprites into images
            AsteroidImage.sprite = AsteroidSprites[ImageCount];

            LastImageRefresh = Time.time;
            ImageCount++;
            if(ImageCount >= AsteroidSprites.Length)
            {
                // Loop back
                ImageCount = 0;
            }
        }
    }
}
