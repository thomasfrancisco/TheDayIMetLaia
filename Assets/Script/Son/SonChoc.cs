﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonChoc : MonoBehaviour {

    public AudioClip collisionSound;

    private AudioSource source;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playCollision()
    {
        if (!source.isPlaying)
        {
            source.clip = collisionSound;
            source.Play();
        }
    }
}
