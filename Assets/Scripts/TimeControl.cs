using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    private float timeRemaining;
    private Text timeText;
    private bool isRunning;
    public GameObject nextStepPrefab;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 10f;
        timeText = GetComponent<Text>();
        timeText.text = formatTime(timeRemaining);
        isRunning = true;
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
                StopPlay();
                ShowResults();
            }
            timeText.text = formatTime(timeRemaining);
        }
        
    }

    void StopPlay()
    {
        GameObject playerFinch = GameObject.FindWithTag("Finch"); //there should be exactly one Finch active
        // Deactivate all control scripts
        MonoBehaviour[] scripts = playerFinch.GetComponents<MonoBehaviour>();
        foreach(MonoBehaviour s in scripts)
        {
            s.enabled = false;
        }
    }

    void ShowResults()
    {
        // Get score
        GameObject playerNest = GameObject.Find("PlayerNest");
        int score =  playerNest.GetComponent<NestControl>().GetScore();
        Debug.Log(score);
        // Create UI message with score
        string message = "You collected " + score + " seeds.";
        GameObject roundOneCanvas = GameObject.Find("Canvas");
        // Fix this hacky positioning later
        GameObject nextStep = Instantiate(nextStepPrefab, new Vector3(0, 100, 0), Quaternion.identity) as GameObject;
        nextStep.GetComponentInChildren<Text>().text = message;
        nextStep.transform.SetParent(roundOneCanvas.transform, false);
        // Give option to continue
    }

    public void LoadRoundTwoSameFinch()
    {
        string currFinch = GameObject.FindGameObjectWithTag("Finch").name;
        GameObject.Find("RoundManager").GetComponent<SceneLoaderScript>().LoadRoundTwo(currFinch);

    }

    string formatTime(float raw)
    {
        int mins = Mathf.FloorToInt(raw / 60);
        string secs = Mathf.FloorToInt(raw % 60).ToString("D2");
        return "Time " + mins + ":" + secs;
    }
}
