﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLogBehaviour : MonoBehaviour {

    public float trigger_angle;
    public float sound_dist;
    public float trigger_dist;
    public bool audioLog_Played;
    public float frequency;
    public AudioClip audioLog_Content;
    public AudioClip[] sequence;
    public AudioClip soundFar;
    public AudioClip soundNear;
    public AudioClip soundAlreadyPlayed;
    public AudioClip soundActivation;
    public bool isFinished;

    private AudioSource source;
    private Transform ugo;
    private RailMovementV2 movementScript;
    private float distance;
    private bool isPlaying;
    private float timer;
    private bool willPlay;
    private bool isSequence;

    private void Awake()
    {
        isFinished = false;
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = false;
        ugo = transform.Find("/Player");
        movementScript = ugo.GetComponent<RailMovementV2>();
        isPlaying = false;
        willPlay = false;
        timer = 0f;
        if (!audioLog_Content)
        {
            isSequence = true;
        }
    }

    // Use this for initialization
    void Start () {
        distance = Vector3.Distance(ugo.position, transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(ugo.position, transform.position);
        timer += Time.deltaTime;

        if (isPlaying)
        {
            if(source.timeSamples >= source.clip.samples - 5)
            {
                audioLog_Played = true;
                isPlaying = false;
            }
        } else if (willPlay)
        {
            if(source.timeSamples >= source.clip.samples - 5)
            {
                willPlay = false;
                isPlaying = true;
                if (!isSequence)
                {
                    source.clip = audioLog_Content;
                    source.Play();
                } else
                {
                    StartCoroutine(playSequence());
                }
            }
        } else if (distance < trigger_dist)
        {
            if(timer > frequency)
            {
                if (!audioLog_Played)
                    source.clip = soundNear;
                else
                    source.clip = soundAlreadyPlayed;
                source.Play();
                timer = 0f;
                
            }
            if(Input.inputString == "\n" || Input.GetButtonDown("Fire1"))
            {
                if (getAngleWithObject(transform) < trigger_angle)
                {
                    movementScript.doAction = false;
                    
                    source.clip = soundActivation;
                    source.Play();
                    willPlay = true;
                }
            }

        } else if (distance < sound_dist)
        {
            if(timer > frequency)
            {
                source.clip = soundFar;
                source.Play();
                timer = 0f;
            }
        }
		
	}

    private float getAngleWithObject(Transform target)
    {
        return Vector3.Angle(target.position - Camera.main.transform.position, Camera.main.transform.forward);
    }
    
    private IEnumerator playSequence()
    {
        isFinished = false;
        for(int i = 0; i < sequence.Length; i++)
        {
            source.clip = sequence[i];
            source.Play();
            yield return new WaitForSeconds(0.5f);
        }
        isFinished = true;
    }
    
}
