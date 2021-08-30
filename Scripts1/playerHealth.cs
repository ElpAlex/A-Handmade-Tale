using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class playerHealth : MonoBehaviour {

    public int fullHealth;
    public int currentHealth;
    public int numOfHearts;

    public GameObject pDeathFX;
    public GameObject gOScreen;

    public Image damageScreen;
    Color flashColor = new Color(255f, 255f, 255f, 1f);
    float flashSpeed = 5f;
    bool damaged = false;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    AudioSource getHitSound;
    AudioSource pickupSound;
    /// </summary>
	// Use this for initialization
	void Start () {
        currentHealth = fullHealth;
        getHitSound = GameObject.FindGameObjectWithTag("hitSound").GetComponent<AudioSource>();
        pickupSound = GameObject.FindGameObjectWithTag("pickupSound").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        if(currentHealth >numOfHearts)
        {
            currentHealth = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i<numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        //damageScreen
        if(damaged)
        {
            damageScreen.color = flashColor;
            
        }
        else
        {
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
	}

    public void addDamage(int damage)
    {
        Instantiate(pDeathFX, transform.position + new Vector3(0,0.5f,0), Quaternion.Euler(new Vector3(-90, 0, 0)));
        currentHealth -= damage;
        getHitSound.Play();
        damaged = true;
        if(currentHealth <= 0)
        {
            makeDead();
        }
    }

    public void makeDead()
    {
        Instantiate(pDeathFX, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        
        StartCoroutine(waitforone());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Life"&& currentHealth<numOfHearts)
        {
            currentHealth++;
            pickupSound.Play();
            Destroy(other.gameObject);
        }
    }

    IEnumerator waitforone()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;
        gOScreen.SetActive(true);
    }
}
