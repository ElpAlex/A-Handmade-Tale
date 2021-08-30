using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class bigFireBall : MonoBehaviour {

    // Use this for initialization
    public bossContainer boss;
    public GameObject ps;
    public PlayerController player;
    //different positions
    public GameObject posUp;
    public GameObject posDown;

    public bool playerTouch;
    public bool shot;
    public GameObject highPlatform;
    public GameObject highPlatform2;
    Animator platformAnim;
    Animator platformAnim2;

    public GameObject text;
    public GameObject fireBallstext;

    public fireFireBalls muzzle;

    AudioSource shootSound;

	void Start () {
        ps.SetActive(false);
        playerTouch = false;
        platformAnim = highPlatform.GetComponent<Animator>();
        platformAnim2 = highPlatform2.GetComponent<Animator>();
        shot = false;
        text.SetActive(false);
        fireBallstext.SetActive(false);

        shootSound = GameObject.FindGameObjectWithTag("shootSound").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (boss.States==bossContainer.Phases.dizzyPhase)
        {
            transform.LookAt(boss.transform.position);
            if (playerTouch && Input.GetKeyDown(KeyCode.E))
            {
                if (muzzle.numOfFireBalls > 5)
                {
                    ps.SetActive(true);
                    ps.transform.LookAt(boss.transform.position);
                    boss.addDamage(10);
                    transform.position = posUp.transform.position;
                    fireBallstext.SetActive(false);
                    muzzle.numOfFireBalls -= 5;
                    shot = true;
                    shootSound.Play();
                }
                else
                    StartCoroutine(uiDisapears());
            }
                

            if (shot)
            {
                StartCoroutine(waitToFall());
               
                
            }
        }
        if(boss.States == bossContainer.Phases.noPhase)
        {
            StartCoroutine(waitToFall2());
        }
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerTouch = true;
            text.SetActive(true);
            print("done");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerTouch = false;
            text.SetActive(false);
            print("undone");
        }
    }

    IEnumerator waitToFall()
    {
        yield return new WaitForSeconds(1.5f);
        
        platformAnim.Play("highPlatform");
        platformAnim2.Play("highPlatform2");
        yield return new WaitForSeconds(2f);
        transform.position = posDown.transform.position;
        platformAnim.Play("highPlatformIdle");
        platformAnim2.Play("highPlatformIdle2");
        ps.SetActive(false);
        shot = false;
    }

    IEnumerator waitToFall2()
    {
        platformAnim.Play("highPlatform");
        platformAnim2.Play("highPlatform2");
        yield return new WaitForSeconds(2f);
        platformAnim.Play("highPlatformIdle");
        platformAnim2.Play("highPlatformIdle2");
    }

    IEnumerator uiDisapears()
    {
        fireBallstext.SetActive(true);
        yield return new WaitForSeconds(2f);
        fireBallstext.SetActive(false);
    }
}
