using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_LoadScene : MonoBehaviour {

	
    public void LoadSceneByName(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void LoadSceneByIndex(int SceneIndexToLoad)
    {
        SceneManager.LoadScene(SceneIndexToLoad);
    }
}
