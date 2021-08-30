using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beeSpawning : MonoBehaviour {

    public GameObject bees;

    public float timeBetweenSpawning;
    private float nextSpawn;

    // Use this for initialization
    void Start()
    {
        nextSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextSpawn < Time.time)
        {
            nextSpawn = Time.time + timeBetweenSpawning;
            Instantiate(bees, transform.position, transform.rotation);
        }
    }
}
