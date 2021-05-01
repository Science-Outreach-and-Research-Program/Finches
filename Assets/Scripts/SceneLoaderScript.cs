using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    // public for now for ease of use
    public string _playerFinch; // which finch to keep active in all rounds
    public GameObject _enemyFinch; // which NPC to have on the island
    public float _largeSpawnRate;
    public float _mediumSpawnRate;
    public float _smallSpawnRate;
    private int currSceneIndex;

    void Start()
    {
        _enemyFinch = null;
        currSceneIndex = 0;
    }

    IEnumerator LoadScene()
    {
        string currScene = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(currScene);
        currSceneIndex += 1;
        SceneManager.LoadScene(currSceneIndex);
        while (SceneManager.GetActiveScene().buildIndex != currSceneIndex)
        {
            yield return null;
        }

        GameObject[] finches = GameObject.FindGameObjectsWithTag("Finch");
        Debug.Log(finches.Length);
        foreach (GameObject f in finches)
        {
            if (f.name != _playerFinch)
            {
                f.SetActive(false);
            }
        }
        PrepIsland();
        // instantiate enemy, if enemy
        if (_enemyFinch != null)
        {
            GameObject enemyFinch = Instantiate(_enemyFinch, new Vector3(-7f, 2.25f, 0f), Quaternion.identity) as GameObject;
        }
    }

    IEnumerator LoadScene(string sceneToLoad)
    {
        string currScene = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(currScene);
        currSceneIndex += 1;
        SceneManager.LoadScene(sceneToLoad);
        while (SceneManager.GetActiveScene().name != sceneToLoad)
        {
            yield return null;
        }

        GameObject[] finches = GameObject.FindGameObjectsWithTag("Finch");
        Debug.Log(finches.Length);
        foreach (GameObject f in finches)
        {
            if (f.name != _playerFinch)
            {
                f.SetActive(false);
            }
        }
        PrepIsland();
        // instantiate enemy, if enemy
        if (_enemyFinch != null)
        {
            GameObject enemyFinch = Instantiate(_enemyFinch, new Vector3(-7f, 2.25f, 0f), Quaternion.identity) as GameObject;
        }
    }

    // called by Finch buttons on StartScene
    public void SetPlayerFinch(string playerFinch)
    {
        _playerFinch = playerFinch;
    }

    // called by Finch buttons on StartScene
    public void SetFirstIsland(string islandType)
    {
        SetIsland(null,3f,1f,1f);
    }

    // called by TimeControl if not changing islands
    public void SetIsland(GameObject enemy)
    {
        if (enemy)
        {
            _enemyFinch = enemy;
        }
    }
    // overloaded version of SetIsland with more flexibility
    public void SetIsland(GameObject enemy, float largeSpawnRate, float mediumSpawnRate, float smallSpawnRate)
    {
        if (enemy)
        {
            _enemyFinch = enemy;
        }
        _largeSpawnRate = largeSpawnRate;
        _mediumSpawnRate = mediumSpawnRate;
        _smallSpawnRate = smallSpawnRate;
    }

    // apply spawn rates to Bush in scene
    public void PrepIsland()
    {
        try
        {
            BushControl bushControl = GameObject.Find("Bush").GetComponent<BushControl>();
            bushControl.largeSpawnRate = _largeSpawnRate;
            bushControl.mediumSpawnRate = _mediumSpawnRate;
            bushControl.smallSpawnRate = _smallSpawnRate;
        }
        catch
        {

        }
    }

    public void LoadNext()
    {
        StartCoroutine(LoadScene());
    }

    public void LoadNext(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

}
