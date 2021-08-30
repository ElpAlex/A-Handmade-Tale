using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTrigger : MonoBehaviour {

    public GameObject camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            

            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y +0.2f, camera.transform.position.z -0.1f);

            camera.transform.rotation = Quaternion.Euler(30, 0, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            camera.transform.rotation = Quaternion.Euler(0, 0, 0);
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y -2, camera.transform.position.z+3);
        }
    }
}
