using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A script that spawns yarn balls (harmful to the player) from the top of the waterfall, along with its damage on the player once they touch the later
public class waterfallBall : MonoBehaviour {

    bool fallen;
    public bool thread;
    public GameObject ball;
    bool spawn;
	// Use this for initialization
	void Start () {
        fallen = false;
        spawn = true;
        if(thread)
            StartCoroutine(newBall(3));
        else
            StartCoroutine(newBall(1.5f));
    }
	
	// Update is called once per frame
	void Update () {
        
        if(fallen && !thread)
        {
            Instantiate(ball, new Vector3(transform.position.x, transform.position.y, transform.position.z -5), 
                Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            StartCoroutine(newBall(3));
        }
        else if (fallen && thread && spawn)
        {
            Instantiate(ball, new Vector3(transform.position.x, transform.position.y, transform.position.z),
                Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            StartCoroutine(newBall(5f));
        }
    }

    IEnumerator newBall(float time)
    {
        fallen = false;
        yield return new WaitForSeconds(time);
        fallen = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            spawn = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            spawn = true;
        }
    }
}

