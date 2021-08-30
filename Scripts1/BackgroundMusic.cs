using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    public AudioClip Level1;
    public AudioClip Level2;
    public AudioClip Level3;
    public AudioSource MusicSource1;
    public AudioSource MusicSource2;
    public AudioSource MusicSource3;

    public PlayerController player;
    void Awake()
    {
        MusicSource1 = GetComponent<AudioSource>();
        MusicSource2 = GetComponent<AudioSource>();
        MusicSource3 = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Update()
    {
        if (player.level1)
        {
            MusicSource1.clip = Level1;
            MusicSource1.Play();
        }
        else if(player.level2)
        {
            MusicSource2.clip = Level2;
            MusicSource1.Stop();
            MusicSource2.Play();
        }
        else if (player.level3)
        {
            MusicSource3.clip = Level3;
            MusicSource2.Stop();
            MusicSource3.Play();
        }
        
    }
}
