using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootFireBall : MonoBehaviour {

    public float damage;
    public float speed;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponentInParent<Rigidbody>();

        if(transform.rotation.y > 0)
        {
            rb.AddForce(Vector3.right * speed, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.right * -speed, ForceMode.Impulse);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy" || other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
        {
            rb.velocity = Vector3.zero;
            enemyHealth theEnemyHealth = other.GetComponent<enemyHealth>();

            if(theEnemyHealth != null)
            {
                theEnemyHealth.addDamage(damage);
            }
            Destroy(gameObject);
        }

    }
}
