using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject targetSeed;
    public bool hasSeed;
    public float speed;
    public GameObject nest;
    public Sprite BeakClosedSprite;
    public Sprite BeakOpenSprite;
    public Vector3 offset;
    public Vector3 nestOffset;
    // Start is called before the first frame update
    void Start()
    {
        hasSeed = false;
        speed = 4.25f;
        findTargetSeed();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        nest = GameObject.Find("NPCNest");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!hasSeed) // go to seed
        {
            if (targetSeed == null || targetSeed.GetComponent<SeedControl>().state != SeedControl.IN_BUSH)
            {
                findTargetSeed();
                spriteRenderer.sprite = BeakOpenSprite; // need to open beak if seed fell out by itself!
            }
            else
            {
                float step = speed * Time.deltaTime;

                // move sprite towards the target location
                transform.position = Vector3.MoveTowards(transform.position, targetSeed.transform.position - offset, step);

                if (Vector3.Distance(transform.position + offset, targetSeed.transform.position) < .25)
                {
                    spriteRenderer.sprite = BeakClosedSprite;

                    GameObject bush = GameObject.Find("Bush");
                    bush.GetComponent<BushControl>().removeSeed(targetSeed);
                    targetSeed.GetComponent<SeedControl>().SetFinch(gameObject, offset);
                    Debug.Log("calling SeedControl.UpdateState");

                    hasSeed = true;
                    nestOffset = new Vector3(Random.Range(-.5f, .5f), 2f, 0f);
                }
            }
        }
        else // back to nest
        {
            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            
            transform.position = Vector3.MoveTowards(transform.position, nest.transform.position + nestOffset - offset, step);
            if (Vector3.Distance(transform.position + offset, nest.transform.position + nestOffset) < .25)
            {
                spriteRenderer.sprite = BeakOpenSprite;
                targetSeed.GetComponent<SeedControl>().SetFinch(null, Vector3.zero);
                hasSeed = false;
            }
        }
    }

    void findTargetSeed()
    {
        GameObject[] seeds = GameObject.FindGameObjectsWithTag("Seed");
        GameObject bestSeed = null;
        float bestSeedScore = float.PositiveInfinity;
        int beakSize = gameObject.GetComponent<BeakSize>().size;
        foreach (GameObject seed in seeds)
        {
            if (seed.GetComponent<SeedControl>().state == SeedControl.IN_BUSH)
            {
                // sizeDiff | score multiplier
                // ---------+-----------
                // 0        | 2
                // 1        | 3
                // 2        | 4
                // a seed one size off would have to be 33% closer than a seed of the right size to be chosen

                int seedSize = seed.GetComponent<SeedControl>().size;
                int sizeDiff = Mathf.Abs(seedSize - beakSize);
                float distance = Vector3.Distance(nest.transform.position, seed.transform.position); //change to distance to nest
                float seedScore = distance * (2f + sizeDiff);
                if (seedScore < bestSeedScore)
                {
                    bestSeed = seed;
                    bestSeedScore = seedScore;
                }
            }
        }
        this.targetSeed = bestSeed;
    }
}
