using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothing = 5f;




    Vector3 offset;
    public PlayerController player;

	// Use this for initialization
	void Start () {
        offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 targetCamPos = target.position + offset;

        if (player.level2)
        {
            if (player.tiltUp)
            {
                transform.position = Vector3.Lerp(new Vector3(target.transform.position.x + 3f, target.transform.position.y + 0.5f, target.transform.position.z + 0.5f), targetCamPos, smoothing * Time.deltaTime);
                transform.rotation = Quaternion.Euler(new Vector3(-15, -90, 0));
            }
            else if (player.tiltDown)
            {
                transform.position = Vector3.Lerp(new Vector3(target.transform.position.x + 3f, target.transform.position.y + 5f, target.transform.position.z + 0.5f), targetCamPos, smoothing * Time.deltaTime);
                transform.rotation = Quaternion.Euler(new Vector3(50, -90, 0));
            }
            else
            {
                transform.position = Vector3.Lerp(new Vector3(target.transform.position.x + 3f, target.transform.position.y + 2.2f, target.transform.position.z + 0.5f), targetCamPos, smoothing * Time.deltaTime);
                transform.rotation = Quaternion.Euler(new Vector3(22, -90, 0));
            }

        }
        else if(player.level3)
        {
            transform.position = Vector3.Lerp(new Vector3(target.transform.position.x - 10f, target.transform.position.y + 9f, target.transform.position.z + 0.5f), targetCamPos, smoothing * Time.deltaTime);
            transform.rotation = Quaternion.Euler(new Vector3(50, 90, 0));
        }
        else
        {
                if (player.zoomOut)
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y,transform.position.z - 0.5f),targetCamPos, smoothing * Time.deltaTime);
            else
                transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }

    }
    
}
