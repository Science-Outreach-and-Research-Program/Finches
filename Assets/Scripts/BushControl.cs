using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushControl : MonoBehaviour
{
    private const float spawnRangeHor = 2f;
    private const float spawnRangeVer = 1.5f;
    private const int maxSeeds = 5;
    private float timeToSpawn;
    private int currSeeds;
    private GameObject[] seeds;
    public GameObject largeSeed;
    public GameObject mediumSeed;
    public GameObject smallSeed;
    

    // Start is called before the first frame update
    void Start()
    {
        //spawnRangeHor = 2f;
        //spawnRangeVer = 1.5f;
        //maxSeeds = 5;
        currSeeds = 0;
        seeds = new GameObject[maxSeeds];
        for (var i = 0; i < maxSeeds; i++)
        {
            seeds[i] = spawnSeed();
        }
        timeToSpawn = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (currSeeds < maxSeeds)
        {
            timeToSpawn -= Time.deltaTime;
            if (timeToSpawn < 0)
            {
                for (var i = 0; i < maxSeeds; i++)
                {
                    if (seeds[i] == null)
                    {
                        seeds[i] = spawnSeed();
                        break;
                    }
                }
                timeToSpawn = 3f;
            }
        }
        
    }

    private GameObject spawnSeed()
    {
        float spawnX = transform.position.x + Random.Range(-spawnRangeHor, spawnRangeHor);
        float spawnY = transform.position.y + Random.Range(-spawnRangeVer, spawnRangeVer);
        currSeeds++;
        return Instantiate(largeSeed, new Vector3(spawnX, spawnY, 0f), Quaternion.identity);
    }

    public void removeSeed(GameObject seed)
    {
        for (var i = 0; i < maxSeeds; i ++)
        {
            if (seeds[i] == seed)
            {
                seeds[i] = null;
                currSeeds--;
                break;
            }
        }
    }
}
