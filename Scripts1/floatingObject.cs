using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingObject : MonoBehaviour {

    public float waterlevel = 0.0f;
    public float floatThreshold = 2.0f;
    public float waterDensity = 0.125f;
    public float downForce = 4.0f;

    float forceFactor;
    Vector3 floatForce;
    
	void FixedUpdate () {
        forceFactor = 1f - ((transform.position.y - waterlevel) / floatThreshold);

        if (forceFactor >0.0f)
        {
            floatForce = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * waterDensity);
            floatForce += new Vector3(0f, -downForce, 0.0f);
            GetComponent<Rigidbody>().AddForceAtPosition(floatForce, transform.position);
        }
	}
	
}
