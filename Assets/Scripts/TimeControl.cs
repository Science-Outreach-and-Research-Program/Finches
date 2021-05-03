using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    private float timeRemaining;
    private Text timeText;
    private bool isRunning;
    private int quota;
    public GameObject nextStepPrefab;
    public GameObject _largeNPCPrefab;
    public GameObject _mediumNPCPrefab;
    public GameObject _smallNPCPrefab;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 40f;
        timeText = GetComponent<Text>();
        timeText.text = formatTime(timeRemaining);
        isRunning = true;
        quota = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                isRunning = false;
                StopPlay("Finch");
                StopPlay("NPC");
                ShowResults();
            }
            timeText.text = formatTime(timeRemaining);
        }
        
    }

    void StopPlay(string tag)
    {
        GameObject finch = GameObject.FindWithTag(tag); //there should be at most one active
        if (finch)
        {
            // Deactivate all control scripts
            MonoBehaviour[] scripts = finch.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour s in scripts)
            {
                s.enabled = false;
            }
        }
        
    }

    void ShowResults()
    {
        SceneLoaderScript sceneLoaderScript = GameObject.Find("RoundManager").GetComponent<SceneLoaderScript>();
        // Get score
        GameObject playerNest = GameObject.Find("PlayerNest");
        int score =  playerNest.GetComponent<NestControl>().GetScore();
        string message = "";
        // Create UI message with score
        if (score >= quota)
        {
            message = "You collected enough seeds, you are thriving on this island!";
            sceneLoaderScript.SetIsland(_largeNPCPrefab);
        }
        else
        {
            message = "You didn't collect enough seeds, so you must migrate to another island!";
            sceneLoaderScript.SetIsland(_mediumNPCPrefab,2f,7f,1f);
        }
        GameObject roundOneCanvas = GameObject.Find("Canvas");
        // Fix this hacky positioning later
        GameObject nextStep = Instantiate(nextStepPrefab, new Vector3(0, 100, 0), Quaternion.identity) as GameObject;
        nextStep.GetComponentInChildren<Text>().text = message;
        nextStep.transform.SetParent(roundOneCanvas.transform, false);
        // Give option to continue
        Button continueButton = nextStep.GetComponentInChildren<Button>();
        continueButton.onClick.AddListener(delegate { sceneLoaderScript.LoadNext(); });
    }


    string formatTime(float raw)
    {
        int mins = Mathf.FloorToInt(raw / 60);
        string secs = Mathf.FloorToInt(raw % 60).ToString("D2");
        return "Time " + mins + ":" + secs;
    }
}
