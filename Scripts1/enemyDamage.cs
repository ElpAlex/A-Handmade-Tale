using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour {

    public int damage;
    public float damageRate;
    public float pushBackForce;

    float nextDamage;
    bool playerInRange = false;

    GameObject player;
    playerHealth pHealth;
    // Use this for initialization
    void Start () {
        nextDamage = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
        pHealth = player.GetComponent<playerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerInRange)
        {
            Attack();
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }

    public void Attack()
    {
        if(nextDamage<=Time.time)
        {
            pHealth.addDamage(damage);
            nextDamage = Time.time + damageRate;

            pushBack(player.transform);
        }
    }

    public void pushBack(Transform pushedObject)
    {
        Vector3 pushDirection = new Vector3(pushedObject.position.x - transform.position.x, 0, 0).normalized;
        pushDirection *= pushBackForce;

        Rigidbody pushedRB = pushedObject.GetComponent<Rigidbody>();
        pushedRB.velocity = Vector3.zero;
        pushedRB.AddForce(pushDirection, ForceMode.Impulse);
    }
}
