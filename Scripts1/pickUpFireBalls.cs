using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickUpFireBalls : MonoBehaviour {

    public Text text = null;
    int number;
	// Use this for initialization
	void Start () {
        number = 1000;
    }
	
	// Update is called once per frame
	void Update () {
        text.text = "x " + number.ToString();
    }
}
