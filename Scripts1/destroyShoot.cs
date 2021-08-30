using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyShoot : MonoBehaviour {

    public float aliveTime;

	// Use this for initialization
	void Awake () {
        Destroy(gameObject, aliveTime);
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject,.1f);
        }
    }
}
