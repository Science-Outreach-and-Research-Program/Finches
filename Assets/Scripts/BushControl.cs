using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushControl : MonoBehaviour
{
    public const int LOADED = 0;    // bush loaded into scene, but rates have not been set
    public const int RATES_SET = 1; // rates have been set, but bush is empty
    public const int READY = 2;     // bush has its initial seeds, and is ready for play

    private const float spawnRangeHor = 1f;
    private const float spawnRangeVer = 0.75f;
    private const int maxSeeds = 6;
    private float timeToSpawn;
    private int currSeeds;
    private GameObject[] seeds;
    public GameObject largeSeed;
    public GameObject mediumSeed;
    public GameObject smallSeed;
    public float largeSpawnRate;
    public float mediumSpawnRate;
    public float smallSpawnRate;
    public int state;

    // Start is called before the first frame update
    void Start()
    {
        currSeeds = 0;
        seeds = new GameObject[maxSeeds];
        timeToSpawn = 1.5f;
        state = LOADED;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == READY)
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
                    timeToSpawn = 2.5f;
                }
            }
        }
        else if (state == RATES_SET)
        {
            for (var i = 0; i < maxSeeds; i++)
            {
                seeds[i] = spawnSeed();
            }
            state = READY;
        }
    }

    private GameObject spawnSeed()
    {
        float spawnX = transform.position.x + Random.Range(-spawnRangeHor, spawnRangeHor);
        float spawnY = transform.position.y + Random.Range(-spawnRangeVer, spawnRangeVer);
        currSeeds++;

        float seedTypeRNG = Random.Range(0f, largeSpawnRate + mediumSpawnRate + smallSpawnRate);
        Debug.Log("Seed RNG: " + seedTypeRNG);
        if (seedTypeRNG < largeSpawnRate)
        {
            return Instantiate(largeSeed, new Vector3(spawnX, spawnY, 0f), Quaternion.identity);
        }
        else if (seedTypeRNG < largeSpawnRate + mediumSpawnRate)
        {
            return Instantiate(mediumSeed, new Vector3(spawnX, spawnY, 0f), Quaternion.identity);
        }
        else
        {
            return Instantiate(smallSeed, new Vector3(spawnX, spawnY, 0f), Quaternion.identity);
        }
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
