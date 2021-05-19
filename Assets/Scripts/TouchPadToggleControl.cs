using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchControlToggleControl : MonoBehaviour
{
    public bool enabled;

    public void ToggleTouchPad(string useless)
    {
        GameObject toggleButton = GameObject.Find("ControlToggleButton");
        if (enabled) // enable touch controls
        {
            toggleButton.GetComponentInChildren<Text>().text = "Disable Touch Control";
        }
        else // disable touch controls
        {
            toggleButton.GetComponentInChildren<Text>().text = "Enable Touch Control";
        }

    }
}
