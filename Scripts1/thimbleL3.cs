using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script allows the thimble object in level 3 to remain in place and make the seesaw work properly
public class thimbleL3 : MonoBehaviour {
    Rigidbody rb;
    public bossContainer boss;
    public GameObject highPoint;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if(boss.States == bossContainer.Phases.dizzyPhase)
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
        else
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
            transform.position = highPoint.transform.position;
        }
    }
}
