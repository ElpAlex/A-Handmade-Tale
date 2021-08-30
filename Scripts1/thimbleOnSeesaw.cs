using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A script that changes the mass of the object so that when it is reaches its highest point on the y axis, 
//it becomes heavy and, when it reaches its lowest height is becomes light.
//This way the character would jump on the other side of the seesaw and by jumping they could reach a higher point
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
