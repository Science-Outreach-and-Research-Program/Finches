using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    IEnumerator LoadScene(string sceneToLoad, string playerFinch)
    {
        string currScene = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(currScene);
        SceneManager.LoadScene(sceneToLoad);
        while (SceneManager.GetActiveScene().name != sceneToLoad)
        {
            yield return null;
        }

        GameObject[] finches = GameObject.FindGameObjectsWithTag("Finch");
        Debug.Log(finches.Length);
        foreach(GameObject f in finches)
        {
            if (f.name != playerFinch)
            {
                f.SetActive(false);
            }
            
        }
    }

    public void LoadRoundOne(string playerFinch)
    {
        StartCoroutine(LoadScene("RoundOne", playerFinch));
    }

    public void LoadRoundTwo(string playerFinch)
    {
        StartCoroutine(LoadScene("RoundTwo", playerFinch));
    }
}
