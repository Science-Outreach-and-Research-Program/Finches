using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NestControl : MonoBehaviour
{
    public Text score;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        score.text = "Seeds: " + count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Seed")
        {
            Debug.Log("(Nest) Trigger entered");
            //GetComponent<CapsuleCollider2D>().isTrigger = false;
            count += 1;
            score.text = "Seeds: " + count;
        }

    }
}
