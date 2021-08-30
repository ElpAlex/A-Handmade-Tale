using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class bossContainer : MonoBehaviour {

    public enum Phases
    {
        noPhase,
        jumpingPhase,
        shootingPhase,
        dizzyPhase,
        deathPhase

    }
    public Phases States = Phases.noPhase;
    public bool jumping;
    Rigidbody rb;
    NavMeshAgent agent;
    public boss boss;
    public PlayerController player;
    public float turnspeed;


    int damageCounter = 0;

    public float enemyMaxHealth;
    public float damageModifier;
    public GameObject damageParticles;

    bool centerReached = false;
    bool farReached = false;
    public GameObject centerPoint;
    public GameObject farPoint;

    public float currentHealth;

    public GameObject dizzyPs;

    //timer
    float timeLeft = 20;
    public Text timerText;
    public GameObject thimbleText;
    public GameObject level3Dialog;
    public GameObject portal;
    
    public GameObject touchTrigger;
    playerHealth playerH;

    GameObject growlAudio;
    GameObject machineAudio;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        jumping = true;

        dizzyPs.SetActive(false);
        timerText.enabled = false;
        thimbleText.SetActive(false);
        level3Dialog.SetActive(false);
        portal.SetActive(false);

        currentHealth = enemyMaxHealth;

        playerH = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHealth>();

        growlAudio = GameObject.FindGameObjectWithTag("audioGrowl");
        machineAudio = GameObject.FindGameObjectWithTag("audioMachine");

        growlAudio.SetActive(false);
        machineAudio.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.bossTouched)
        {
            level3Dialog.SetActive(false);
            

            if (States == Phases.noPhase)
            {
                StartCoroutine(goToJumping());
                transform.LookAt(player.transform);
                dizzyPs.SetActive(false);
                timerText.enabled = false;
                thimbleText.SetActive(false);
                growlAudio.SetActive(true);
                machineAudio.SetActive(false);
            }
            if (States == Phases.jumpingPhase)
            {
                StartCoroutine(goToShooting());
                transform.LookAt(player.transform);
                if (jumping == true)
                {
                    StartCoroutine(timeofMoving());

                    StartCoroutine(startOver());
                }
                growlAudio.SetActive(true);
                machineAudio.SetActive(false);
            }
            if (States == Phases.shootingPhase)
            {
                StartCoroutine(goToDizzy());
                //spin around itself
                transform.RotateAround(transform.position, transform.up, Time.deltaTime * turnspeed);
                //if the center of the arena is not reached, go there
                if (!centerReached)
                    agent.SetDestination(centerPoint.transform.position);
                //if the center is reached, stop going to it
                if (agent.transform.position == centerPoint.transform.position)
                {
                    centerReached = true;
                }
                growlAudio.SetActive(false);
                machineAudio.SetActive(true);
            }
            if (States == Phases.dizzyPhase)
            {
                transform.LookAt(player.transform);
                StartCoroutine(goToNoPhase());
                if (!farReached)
                    agent.SetDestination(farPoint.transform.position);
                if (agent.transform.position == farPoint.transform.position)
                {
                    farReached = true;
                }
                dizzyPs.SetActive(true);

                //timer

                timerText.enabled = true;
                thimbleText.SetActive(true);
                timeLeft -= Time.deltaTime;
                if (timeLeft < 0)
                {
                    timeLeft = 20;
                }
                int seconds = (int)timeLeft % 60;
                if (timeLeft <= 15)
                {
                    thimbleText.SetActive(false);
                }

                timerText.text = "Seconds remaining: " + seconds;

                growlAudio.SetActive(false);
                machineAudio.SetActive(false);
            }
            
        }
        else if (!player.bossTouched && player.level3)
        {
            level3Dialog.SetActive(true);
            portal.SetActive(true);
            
            transform.position = touchTrigger.transform.position;
            States = Phases.noPhase;
            growlAudio.SetActive(false);
            machineAudio.SetActive(false);
        }
    }


    IEnumerator timeofMoving()
    {
        yield return new WaitForSeconds(0.5f);

        agent.SetDestination(player.transform.position);
        
        yield return new WaitForSeconds(0.5f);
        jumping = false; 
    }

    IEnumerator startOver()
    {
        yield return new WaitForSeconds(3f);
        jumping = true;
    }
   
    IEnumerator goToJumping()
    {
        yield return new WaitForSeconds(1f);
        States = Phases.jumpingPhase;
    }

    IEnumerator goToShooting()
    {
        yield return new WaitForSeconds(15.2f);
        StopCoroutine(goToJumping());
        States = Phases.shootingPhase;

    }

    IEnumerator goToDizzy()
    {
       
        yield return new WaitForSeconds(15f);
        StopCoroutine(goToShooting());
        States = Phases.dizzyPhase;

    }

    IEnumerator goToNoPhase()
    {
        yield return new WaitForSeconds(20f);
        StopAllCoroutines();
        States = Phases.noPhase;

    }

    
    public void addDamage(float damage)
    {
        damageFX();
        damage *= damageModifier;

        if (damage <= 0f)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            makeDead();
        }
    }

    public void damageFX()
    {
        Instantiate(damageParticles, transform.position + new Vector3(-3, 2, -0.5f), transform.rotation);
    }

    void makeDead()
    {
        Destroy(gameObject.transform.root.gameObject,3.15f);

        StopAllCoroutines();
        States = Phases.deathPhase;
        growlAudio.SetActive(false);
        machineAudio.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fireBallProjectile")
        {
            addDamage(10);
        }

        //set the destination to the position of the boss, in order to avoid weird behaviour when it reaches the player
        if (other.tag=="Player"&&jumping)
        {
            agent.SetDestination(transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player"&&jumping)
        {
            agent.SetDestination(player.transform.position);
        }
    }
}

