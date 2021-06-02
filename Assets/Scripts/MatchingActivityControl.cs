using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingActivityControl : MonoBehaviour
{
    public GameObject largeSeed;
    public GameObject mediumSeed;
    public GameObject smallSeed;
    public GameObject continueButton;
    // Update is called once per frame
    void Update()
    {
        if (largeSeed.GetComponent<DragDropControl>().matched &&
            mediumSeed.GetComponent<DragDropControl>().matched &&
            smallSeed.GetComponent<DragDropControl>().matched)
        {
            continueButton.SetActive(true);
        }
    }
}
