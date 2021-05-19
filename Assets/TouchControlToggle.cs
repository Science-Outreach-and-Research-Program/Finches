using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchControlToggle : MonoBehaviour
{
    public bool enabled;
    public GameObject joystick;
    public GameObject joybutton;

    public void ToggleTouchPad()
    {
        GameObject toggleButton = GameObject.Find("ControlToggleButton");

        if (enabled) // disable touch controls
        {
            toggleButton.GetComponentInChildren<Text>().text = "Disable Touch Controls";
            enabled = false;

        }
        else // enable touch controls
        {
            toggleButton.GetComponentInChildren<Text>().text = "Enable Touch Controls";
            enabled = true;
        }
        joystick.SetActive(enabled);
        joybutton.SetActive(enabled);
    }
}
