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
    public static readonly Vector3 offset = new Vector3(1.5f, -0.08f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        hasSeed = false;
        speed = 5f;
        findTargetSeed();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        nest = GameObject.Find("NPCNest");
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSeed)
        {
            if (targetSeed == null || targetSeed.GetComponent<SeedControl>().state != SeedControl.IN_BUSH)
            {
                findTargetSeed();
            }
            else
            {
                float step = speed * Time.deltaTime;

                // move sprite towards the target location
                transform.position = Vector3.MoveTowards(transform.position, targetSeed.transform.position - offset, step);

                if (Vector3.Distance(transform.position + offset, targetSeed.transform.position) < 1)
                {
                    spriteRenderer.sprite = BeakClosedSprite;

                    GameObject bush = GameObject.Find("Bush");
                    bush.GetComponent<BushControl>().removeSeed(targetSeed);
                    targetSeed.GetComponent<SeedControl>().SetFinch(gameObject, offset);
                    Debug.Log("calling SeedControl.UpdateState");

                    hasSeed = true;
                }
            }
        }
        else
        {
            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            Vector3 nestOffset = new Vector3(0f, 2f, 0f);
            transform.position = Vector3.MoveTowards(transform.position, nest.transform.position + nestOffset - offset, step);
            if (Vector3.Distance(transform.position + offset, nest.transform.position + nestOffset) < 1)
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
        GameObject closestSeed = null;
        float closestDistance = float.PositiveInfinity;
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
        this.targetSeed = closestSeed;
    }
}
