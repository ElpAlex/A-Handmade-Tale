using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {
    
    public enemyHealth self;

    public float speed;
    public bool facingRight = true;

    float movespeed;

    Rigidbody rb;
    Animator anim;

    public bool worm;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        movespeed = speed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!facingRight)
            rb.velocity = new Vector3((movespeed * -1), rb.velocity.y, 0);
        else
            rb.velocity = new Vector3(movespeed, rb.velocity.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.tag == "obstacle" || other.tag=="enemy") && worm)
        {
            Flip();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle" && !worm)
        {
            self.damageFX();
            Destroy(transform.parent.gameObject, 0.5f);

        }
    }
    
    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = self.transform.localScale;
        theScale.z *= -1;
        self.transform.localScale = theScale;

    }
}
