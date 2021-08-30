using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thimbleOnSeesaw : MonoBehaviour {

    Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "floor")
        {
            rb.mass = 1;
        }
        else if (other.tag == "air")
        {
            rb.mass = 20;
        }
    }
}
