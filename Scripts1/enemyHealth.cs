using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour {

    public float enemyMaxHealth;
    public float damageModifier;
    public GameObject damageParticles;
    public enemyController self;
    public GameObject drop;
    public bool drops;
    AudioSource deathSound;

    public float currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = enemyMaxHealth;
        deathSound = GameObject.FindGameObjectWithTag("deathSound").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
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

        if(currentHealth <= 0)
        {
            makeDead();
        }
    }

    public void damageFX()
    {
        if (self.worm)
            Instantiate(damageParticles, transform.position + new Vector3(0, 1, 0), transform.rotation);
        else
        {
            if(self.facingRight)
                Instantiate(damageParticles, transform.position + new Vector3(-3, 1, 0), transform.rotation);

            else
                Instantiate(damageParticles, transform.position + new Vector3(3, 1, 0), transform.rotation);
        }
    }

    void makeDead()
    {

        Destroy(gameObject.transform.root.gameObject,0.2f);
        if (drops)
        {
            Instantiate(drop, transform.position + new Vector3(0,1,0), Quaternion.Euler(new Vector3(-90,0,0)));
        }

    }
}
