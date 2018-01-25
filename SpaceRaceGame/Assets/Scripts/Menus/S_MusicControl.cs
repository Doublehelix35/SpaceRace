using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MusicControl : MonoBehaviour {

    bool IsMuted = false;
    float Volume = 1f;
    public AudioSource Music;

    void Start()
    {
        if(PlayerPrefs.GetInt("IsMuted") == 0)
        {
            IsMuted = false;
        }
        else
        {
            IsMuted = true;
        }

        if (PlayerPrefs.HasKey("Volume"))
        {
            Music.volume = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            Music.volume = Volume;
        }
        
        
        
        

        if (IsMuted)
        {
            Music.Pause();
        }
    }

    void Update()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            Music.volume = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            Music.volume = Volume;
        }

        if (IsMuted)
        {
            Music.Pause();
        }
        else
        {
            Music.UnPause();
        }
    }

    public void ToggleMute()
    {
        IsMuted = !IsMuted;

        // Save is muted state
        if (IsMuted)
        {
            PlayerPrefs.SetInt("IsMuted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("IsMuted", 0);
        }
        
    }

    public void SetVolume(float volume)
    {
        Volume = volume;

        // Save volume
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
