using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_LoadScene : MonoBehaviour {

    public bool NextSceneAfterDelay = false;
    public float Delay = 3f;
    public string NextSceneName;

    private void Start()
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

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(Delay);
        SceneManager.LoadScene(NextSceneName);
    }
}
