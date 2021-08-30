using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public AudioClip MusicClip;
    public AudioSource MusicSource;

    void Awake()
    {
        MusicSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        MusicSource.clip = MusicClip;
        MusicSource.Play();
    }
}


