using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_LevelSelect : MonoBehaviour {

    // UI images to change
    public Image AsteroidImage;
    public Image SnakeImage;
    public Image PacMonsterImage;
    
    // Title
    public Text Title;
    float SizeToScale = 0.2f;

    // Should all be the same length
    public Sprite[] AsteroidSprites;
    public Sprite[] SnakeSprites;
    public Sprite[] PacMonsterSprites;

    public float ImageRefreshTimer = 1f; // Time between image change
    float LastImageRefresh; // Time.time of last refresh
    int ImageCount = 1; // What image to show

    void Start ()
    {
        LastImageRefresh = Time.time;	
	}
	
	void Update ()
    {
        // Ping pong between 1f and 1f + size to scale
        Vector3 ScaledVec = new Vector3(Mathf.PingPong(Time.time / 6, SizeToScale) + 1f,
                                        Mathf.PingPong(Time.time / 6, SizeToScale) + 1f,
                                        0f);
        Title.rectTransform.localScale = ScaledVec;

        // Refresh images
        if(LastImageRefresh + ImageRefreshTimer < Time.time)
        {
            // Load new sprites into images
            AsteroidImage.sprite = AsteroidSprites[ImageCount];
            SnakeImage.sprite = SnakeSprites[ImageCount];
            PacMonsterImage.sprite = PacMonsterSprites[ImageCount];

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
