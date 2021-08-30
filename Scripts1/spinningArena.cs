using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinningArena : MonoBehaviour {
    public float speed = 5;
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime *speed );
    }
}
