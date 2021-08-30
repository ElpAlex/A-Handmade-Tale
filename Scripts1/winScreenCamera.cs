using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winScreenCamera : MonoBehaviour {
    public float turnspeed;
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * turnspeed);
    }
}
