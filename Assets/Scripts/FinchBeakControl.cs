using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinchBeakControl : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite BeakClosedSprite;
    public Sprite BeakOpenSprite;
    public GameObject seed;
    public static readonly Vector3 offset = new Vector3(1.5f, 0.08f, -0.1f);

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
            Debug.Log("Space key was pressed.");
            spriteRenderer.sprite = BeakClosedSprite;

            
            GameObject[] seeds = GameObject.FindGameObjectsWithTag("Seed");
            GameObject closestSeed = null;
            float closestDistance = 1;
            foreach (GameObject seed in seeds)
            {
                float distance = Vector3.Distance(transform.position - offset, seed.transform.position);
                if (distance < closestDistance)
                {
                    closestSeed = seed;
                }
            }
            this.seed = closestSeed;
            if (seed)
            {
                // seed.GetComponent<SeedControl>().UpdateState(SeedControl.IN_BEAK, 100);
                seed.GetComponent<SeedControl>().SetFinch(gameObject);
                Debug.Log("calling SeedControl.UpdateState");
            }
            else
            {
                Debug.Log("No valid seed detected");
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Space key was released.");
            spriteRenderer.sprite = BeakOpenSprite;

            if (seed)
            {
                // seed.GetComponent<SeedControl>().UpdateState(SeedControl.FALLING, 0);
                seed.GetComponent<SeedControl>().SetFinch(null);
                seed = null;
            }
        }
    }
}
