using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetStartScene : MonoBehaviour
{
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
        Debug.Log("THERE ARE NO EASTER EGGS HERE. GO AWAY.");
    }
}
