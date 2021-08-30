using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMuzzle : MonoBehaviour {
    public bossContainer boss;
    public float timeBetweenBullets;
    private float nextBullet;

    [SerializeField]
    private GameObject[] bullets;
    private int randomBullets;

    public bool shoot;
    // Use this for initialization
    void Start () {
        nextBullet = 0;
        shoot = false;
    }

    // Update is called once per frame
    void Update () {

        if (boss.States == bossContainer.Phases.shootingPhase)
        {
            StartCoroutine(startShooting());
            if (nextBullet < Time.time && shoot)
            {
                nextBullet = Time.time + timeBetweenBullets;

                randomBullets = Random.Range(0, 3);
                
                Instantiate(bullets[randomBullets], transform.position, transform.rotation);
            }
        }
    }

    IEnumerator startShooting()
    {
        yield return new WaitForSeconds(1f);
        shoot = true;
    }
}
