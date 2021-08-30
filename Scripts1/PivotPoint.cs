using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotPoint : MonoBehaviour {

    public PlayerController player;

	// Update is called once per frame
	void Update () {
        if (GameObject.Find("final puppet@Running (2)"))
            transform.position = player.transform.position;
    }
}
