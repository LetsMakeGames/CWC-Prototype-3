using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private PlayerController playerControllerScript;
    private float counter = 0.0f;
    private int repeatRateMin = 2;    
    private int repeatRateMax = 5;
    private int spawnTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Randomize spawn time.
        Randomizer();
    }

    // Update is called once per frame
    void Update()
    {
        // Update counter with last frame time delta for triggering spawns at spawn times.
        counter += Time.deltaTime;

        // If counter exceeds spawn time, spawn object, randmoize spawn time, reset counter.
        if (counter >= spawnTime)
        {
            SpawnObject();
            Randomizer();
            counter = 0;
        }
    }

    // Spawn object prefab at spawnPos 
    void SpawnObject()
    {
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
        
    }

    // Randomize spawn times.
    private void Randomizer()
    {
        spawnTime = Random.Range(repeatRateMin, repeatRateMax);
    }
}
