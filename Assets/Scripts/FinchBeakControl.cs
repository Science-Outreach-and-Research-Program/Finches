using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinchBeakControl : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite BeakClosedSprite;
    public Sprite BeakOpenSprite;
    public GameObject seed;
    public GameObject joybutton;

    private int state;
    private const int OPEN = 0;
    private const int RELEASE = 1;
    private const int CLOSE = 2;
    private const int GRAB = 3;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //BeakClosedSprite = Resources.Load("Sprites/large_beak_close_cutout.png") as Sprite;
        //BeakOpenSprite = Resources.Load("Sprites/large_beak_open_cutout.png") as Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Space key was pressed.");
            state = GRAB;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //Debug.Log("Space key was released.");
            state = RELEASE;
        }

        if (state == GRAB)
        {
            spriteRenderer.sprite = BeakClosedSprite;

            GameObject[] seeds = GameObject.FindGameObjectsWithTag("Seed");
            GameObject closestSeed = null;
            float closestDistance = .5f;
            foreach (GameObject seed in seeds)
            {
                if (seed.GetComponent<SeedControl>().state == SeedControl.IN_BUSH)
                {
                    float distance = Vector3.Distance(transform.position + offset, seed.transform.position);
                    if (distance < closestDistance)
                    {
                        closestSeed = seed;
                        closestDistance = distance;
                    }
                }
            }
            this.seed = closestSeed;
            if (seed)
            {
                GameObject bush = GameObject.Find("Bush");
                bush.GetComponent<BushControl>().removeSeed(seed);
                // seed.GetComponent<SeedControl>().UpdateState(SeedControl.IN_BEAK, 100);
                seed.GetComponent<SeedControl>().SetFinch(gameObject, offset);
                //Debug.Log("calling SeedControl.UpdateState");
            }
            else
            {
                //Debug.Log("No valid seed detected");
            }
            state = CLOSE;
        }
        else if (state == RELEASE)
        {    
            spriteRenderer.sprite = BeakOpenSprite;

            if (seed)
            {
                // seed.GetComponent<SeedControl>().UpdateState(SeedControl.FALLING, 0);
                seed.GetComponent<SeedControl>().SetFinch(null, Vector3.zero);
            }
            state = OPEN;
        }
    }
}
