using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DragDropControl : MonoBehaviour
{
    public bool isDragging = false;
    private Vector2 startPosition;
    public string size;
    public GameObject largeFinchMatch;
    public GameObject mediumFinchMatch;
    public GameObject smallFinchMatch;
    private float hitboxSize = 1.2f;

    public bool matched = false;

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                             Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                                             GameObject.Find("Canvas").transform.position.z);
        }
    }

    public void OnMouseDown()
    {
        isDragging = true;
        //Debug.Log("start drag");
    }

    public void OnMouseUp()
    {
        isDragging = false;

        float distanceLarge = Vector3.Distance(transform.position, largeFinchMatch.transform.position);
        float distanceMedium = Vector3.Distance(transform.position, mediumFinchMatch.transform.position);
        float distanceSmall = Vector3.Distance(transform.position, smallFinchMatch.transform.position);
        //Debug.Log(distanceLarge);
        //Debug.Log(distanceMedium);
        //Debug.Log(distanceSmall);
        // could be more elegant, but that would take more dev time
        string message = "";
        if (distanceLarge < Math.Min(distanceSmall, distanceMedium) && distanceLarge < hitboxSize)
        {
            if (size == "large")
            {
                message = "Nice! G. magniorostris is best suited for this seed.";
                matched = true;
            }
            if (size == "medium")
            {
                message = "This seed doesn't provide enough nutrition for a large finch like G. magnirostris.";
                matched = false;
            }
            if (size == "small")
            {
                message = "G. magnirostris' beak is too clumsy to pick up this small seed.";
                matched = false;
            }
                
            largeFinchMatch.transform.Find("Feedback").GetComponentInChildren<Text>().text = message;
        }
        else if (distanceMedium < Math.Min(distanceLarge, distanceSmall) && distanceMedium < hitboxSize)
        {
            if (size == "large")
            {
                message = "This seed is too troublesome for G. fortis to crack open.";
                matched = false;
            }
            if (size == "medium")
            {
                message = "Very good! G. fortis is best suited for this seed.";
                matched = true;
            }
            if (size == "small")
            {
                message = "G. fortis could obtain more energy from a different seed.";
                matched = false;
            }
            mediumFinchMatch.transform.Find("Feedback").GetComponentInChildren<Text>().text = message;
        }
        else if (distanceSmall < Math.Min(distanceLarge, distanceMedium) && distanceSmall < hitboxSize)
        {
            if (size == "large")
            {
                message = "G. parvula cannot carry or crack open this large seed.";
                matched = false;
            }
            if (size == "medium")
            {
                message = "G. parvula cannot carry or crack open this seed.";
                matched = false;
            }
            if (size == "small")
            {
                message = "Yes! G. parvula is best suited for this seed.";
                matched = true;
            }
            smallFinchMatch.transform.Find("Feedback").GetComponentInChildren<Text>().text = message;
        }
        Debug.Log(message);
    }
}
