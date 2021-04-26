using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NestControl : MonoBehaviour
{
    //public Text score;
    private int count;
    private TextMesh scoreDisplay;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        scoreDisplay = GetComponentInChildren<TextMesh>();
        scoreDisplay.text = "Seeds: " + count;
        // score.text = "Seeds: " + count;
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
            if (other.GetComponent<Rigidbody2D>().velocity.y > -8)
            {
                other.GetComponent<SeedControl>().UpdateState(SeedControl.IN_NEST, 100);
                Debug.Log("(Seed) caught by nest");
                count += 1;
            }
            else
            {
                Debug.Log("(Seed) too fast to be caught by nest");
            }
            //GetComponent<CapsuleCollider2D>().isTrigger = false;
            
            scoreDisplay.text = "Seeds: " + count;
            //score.text = "Seeds: " + count;
        }

    }

    public int GetScore()
    {
        return count;
    }
}
