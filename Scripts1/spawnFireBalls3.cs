using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFireBalls3 : MonoBehaviour {
    public GameObject fireballs;
    public PlayerController player;

    public float timeBetweenSpawning;
    private float nextSpawn;

    // Use this for initialization
    void Start () {
        nextSpawn = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (nextSpawn < Time.time && player.bossTouched)
        {
            nextSpawn = Time.time + timeBetweenSpawning;
            Instantiate(fireballs, transform.position, transform.rotation);
        }
	}
}
