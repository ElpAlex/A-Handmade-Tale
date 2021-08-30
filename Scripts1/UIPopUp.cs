using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopUp : MonoBehaviour {

    public GameObject popUpText;
	// Use this for initialization
	void Start () {
        popUpText.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            popUpText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            popUpText.SetActive(false);
        }
    }
}
