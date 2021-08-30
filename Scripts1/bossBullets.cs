using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBullets : MonoBehaviour {
    Rigidbody rb;
    public GameObject child;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = transform.forward * 10;
        child.transform.RotateAround(transform.position, transform.up, Time.deltaTime * -180);
        child.transform.RotateAround(transform.position, transform.forward, Time.deltaTime * -180);
        child.transform.RotateAround(transform.position, transform.right, Time.deltaTime * -180);
    }
}
