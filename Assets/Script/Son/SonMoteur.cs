﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonMoteur : MonoBehaviour {

    public AudioClip moveForwardStart;
    public AudioClip moveForwardLoop;
    public AudioClip moveForwardEnd;

    private AudioSource source;
    private RailMovement UgoMovementScript;

    private bool lastUgoForwardState;

    private void Awake()
    {
        UgoMovementScript = GetComponentInParent<RailMovement>();
        source = GetComponent<AudioSource>();
        lastUgoForwardState = false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(UgoMovementScript.isMovingForward && !lastUgoForwardState)
        {
            //Debut
            source.clip = moveForwardStart;
            source.loop = false;
            source.Play();
        }

        if(UgoMovementScript.isMovingForward && lastUgoForwardState)
        {
            //Loop
            if(source.clip == moveForwardStart)
            {
                //On laisse le temps de se finir
                if(source.timeSamples == source.clip.samples)
                {
                    //MoveForwardStart fini
                    source.clip = moveForwardLoop;
                    source.loop = true;
                    source.Play();
                }
            }
        }

        if(!UgoMovementScript.isMovingForward && !lastUgoForwardState)
        {
            //Fin
            if (source.clip == moveForwardStart || source.clip == moveForwardLoop)
            {
                source.clip = moveForwardEnd;
                source.loop = false;
                source.Play();
            }
        }

        lastUgoForwardState = UgoMovementScript.isMovingForward;
	}
    
}
