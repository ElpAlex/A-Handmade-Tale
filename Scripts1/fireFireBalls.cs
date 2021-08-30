using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fireFireBalls : MonoBehaviour {

    public float timeBetweenBullets= 0.40f;
    public GameObject projectile;

    private float nextBullet;

    public bool readyToThrow;
    bool instantiated = false;
    Vector3 rot;

    public int numOfFireBalls;
    public Text text = null;
    public bool touched = false;

    AudioSource pickupSound;

    // Use this for initialization
    void Awake () {
        nextBullet = 0;
        readyToThrow = false;
        numOfFireBalls = 10;

        pickupSound = GameObject.FindGameObjectWithTag("pickupSound").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        PlayerController myPlayer = transform.root.GetComponent<PlayerController>();
        

        if (Input.GetKeyDown(KeyCode.E) && nextBullet < Time.time && numOfFireBalls>0 && myPlayer.level1 && myPlayer.grounded)
        {
            StartCoroutine(throwSphere());
            nextBullet = Time.time + timeBetweenBullets;

            if (myPlayer.level1)
            {
                if (myPlayer.GetFacing() == -1f)
                {
                    rot = new Vector3(0, -90, 0);
                }
                else
                {
                    rot = new Vector3(0, 90, 0);
                }
            }

            numOfFireBalls--;
        }

        text.text = "x " + numOfFireBalls.ToString();

        if (readyToThrow && !instantiated)
        {
            if (myPlayer.level1)
            {
                Instantiate(projectile, transform.position, Quaternion.Euler(rot));
            }

            instantiated = true;
            StartCoroutine(stopThrowing());
        }
    }

    IEnumerator throwSphere()
    {
        readyToThrow = false;
        yield return new WaitForSeconds(0.4f);
        readyToThrow = true;
        StartCoroutine(stopThrowing());
    }

    IEnumerator stopThrowing()
    {
        readyToThrow = true;
        yield return new WaitForSeconds(0.001f);
        readyToThrow = false;
        instantiated = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireBall")
        {
            numOfFireBalls++;
            touched = true;
            pickupSound.Play();
            Destroy(other.gameObject);
        }
    }
}
