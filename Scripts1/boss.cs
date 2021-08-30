using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class boss : MonoBehaviour {

    Animator anim;
    public bool jumping;
    public GameObject destination;
    public bossContainer container;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {

        switch(container.States)
        {
            case bossContainer.Phases.noPhase:
                {
                    anim.Play("boss idle");
                }
            break;

            case bossContainer.Phases.jumpingPhase:
                {
                    anim.Play("boss jump");
                }
            break;

            case bossContainer.Phases.shootingPhase:
                {
                    anim.Play("boss attack2");
                }
            break;
            case bossContainer.Phases.dizzyPhase:
                {
                    anim.Play("boss idle");
                }
             break;
            case bossContainer.Phases.deathPhase:
                {
                    anim.Play("boss death");
                }
            break;
        }        
    }

   
}
