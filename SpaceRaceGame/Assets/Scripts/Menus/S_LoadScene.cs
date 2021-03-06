﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_LoadScene : MonoBehaviour {

    public bool NextSceneAfterDelay = false; // Automatically go to next scene
    public float Delay = 3f;
    public string NextSceneName;

    void Start()
    {
        if (NextSceneAfterDelay)
        {
            StartCoroutine("NextScene");
        }        
    }

    public void LoadSceneByName(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void LoadSceneByIndex(int SceneIndexToLoad)
    {
        SceneManager.LoadScene(SceneIndexToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(Delay);
        SceneManager.LoadScene(NextSceneName);
    }
}
