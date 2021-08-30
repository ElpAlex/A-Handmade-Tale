using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleaner : MonoBehaviour {
    public GameObject fadeScreen;
    public PlayerController myPlayer;

	// Use this for initialization
	void Start () {
        fadeScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag== "Player")
        {
            playerHealth playerDead = other.gameObject.GetComponent<playerHealth>();
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (playerDead.currentHealth != 0)
            {
                fadeScreen.SetActive(true);
                
                playerDead.addDamage(1);
                if (player.level1 && !player.checkpointArea)
                {
                    playerDead.transform.position = new Vector3(795.9341f, 120.5809f, -234.837f);
                    StartCoroutine(fadeScreenWait());
                }
                else if (player.level1 && player.checkpointArea)
                {
                    playerDead.transform.position = new Vector3(797.6f, 112, -234.8f);
                    StartCoroutine(fadeScreenWait());
                }
                else if(player.level2)
                {
                    playerDead.transform.position = new Vector3(702, 121, -234.8f);
                    player.runSpeed = 2f;
                    player.speedUp = false;
                    
                    StartCoroutine(fadeScreenWait2());
                }
                else if (player.level3)
                {
                    player.transform.position = new Vector3(253.6491f, 100f, -445.01f);
                }
            }
            else if (playerDead.currentHealth <= 0)
                fadeScreen.SetActive(false);
        }
    }

    IEnumerator fadeScreenWait()
    {
        yield return new WaitForSeconds(1);
        fadeScreen.SetActive(false);
    }

    IEnumerator fadeScreenWait2()
    {
        yield return new WaitForSeconds(1);
        fadeScreen.SetActive(false);
    }
}
