﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLogBehaviour : MonoBehaviour
{

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
    private bool isSequence;
    private float volumeInspector;

    private void Awake()
    {
        isFinished = false;
        
        source = GetComponent<AudioSource>();
        volumeInspector = source.volume;
        source.playOnAwake = false;
        source.loop = false;
        ugo = transform.Find("/Player");
        movementScript = ugo.GetComponent<RailMovementV2>();
        isPlaying = false;
        timer = 0f;
        if (!audioLog_Content)
        {
            isSequence = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, trigger_dist);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, sound_dist);
    }

    // Use this for initialization
    void Start()
    {
        distance = Vector3.Distance(ugo.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(ugo.position, transform.position);
        timer += Time.deltaTime;

        if (!isPlaying)
        {
            if (distance < trigger_dist)
            {
                if (timer > frequency)
                {
                    if (!audioLog_Played)
                        source.clip = soundNear;
                    else
                        source.clip = soundAlreadyPlayed;
                    source.volume = volumeInspector;
                    source.Play();
                    timer = 0f;

                }
                if (Input.inputString == "\n" || Input.GetButtonDown("Fire1"))
                {
                    if (getAngleWithObject(transform) < trigger_angle)
                    {
                        movementScript.doAction = false;
                        audioLog_Played = true;
                        source.clip = soundActivation;
                        source.volume = volumeInspector;
                        source.Play();
                        if (isSequence)
                        {
                            StartCoroutine(playSequence(soundActivation.length));
                        }
                        else
                        {
                            StartCoroutine(playContent(soundActivation.length));
                        }
                    }
                }

            }
            else if (distance < sound_dist)
            {
                if (timer > frequency)
                {
                    source.clip = soundFar;
                    source.volume = volumeInspector;
                    source.Play();
                    timer = 0f;
                }
            }
        }

    }

    private float getAngleWithObject(Transform target)
    {
        Vector3 targetCameraVec = target.position - Camera.main.transform.position;
        Vector2 targetCamVec2 = new Vector2(targetCameraVec.x, targetCameraVec.z);
        Vector2 forward = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
        return Vector2.Angle(targetCamVec2, forward);
    }

    private IEnumerator playContent(float time)
    {
        isFinished = false;
        isPlaying = true;
        yield return new WaitForSeconds(time);
        source.volume = 1f;
        source.clip = audioLog_Content;
        source.Play();
        StartCoroutine(currentlyPlaying(audioLog_Content.length));
    }
    private IEnumerator currentlyPlaying(float time)
    {
        yield return new WaitForSeconds(time);
        isPlaying = false;
        isFinished = true;
    }

    private IEnumerator playSequence(float time)
    {
        isPlaying = true;
        yield return new WaitForSeconds(time);
        isFinished = false;
        for (int i = 0; i < sequence.Length; i++)
        {
            source.clip = sequence[i];
            source.Play();
            yield return new WaitForSeconds(0.5f);
        }
        isFinished = true;
        isPlaying = false;
    }

}
