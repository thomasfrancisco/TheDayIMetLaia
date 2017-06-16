using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSound : MonoBehaviour {

    public AudioClip start;
    public AudioClip loop;
    public AudioClip end;
    public AudioClip doorClose;
    public AudioClip doorOpen;
    public Transform door1;
    public Transform door2;

    private AudioSource sourceElevator;
    private AudioSource sourceDoor1;
    private AudioSource sourceDoor2;
    private float volumeInspector;

    private void Awake()
    {
        sourceElevator = GetComponent<AudioSource>();
        sourceDoor1 = door1.GetComponent<AudioSource>();
        sourceDoor2 = door2.GetComponent<AudioSource>();
        sourceElevator.playOnAwake = false;
        sourceElevator.loop = false;
        sourceDoor1.playOnAwake = false;
        sourceDoor1.loop = false;
        sourceDoor2.playOnAwake = false;
        sourceDoor1.loop = false;
             
    }

    public void playElevator()
    {
        StartCoroutine(play());
    }

    public void stopElevator()
    {
        StartCoroutine(stop());
    }

    private IEnumerator play()
    {
        sourceDoor1.clip = doorClose;
        sourceDoor1.Play();
        yield return new WaitForSeconds(doorClose.length + 2f);
        sourceElevator.clip = start;
        sourceElevator.loop = false;
        sourceElevator.Play();
        yield return new WaitForSeconds(start.length);
        sourceElevator.loop = true;
        sourceElevator.clip = loop;
        sourceElevator.Play();
    }

    private IEnumerator stop()
    {
        sourceElevator.clip = end;
        sourceElevator.loop = false;
        sourceElevator.Play();
        yield return new WaitForSeconds(end.length + 2f);
        sourceDoor2.clip = doorOpen;
        sourceDoor2.Play();
    }

    public bool isPlaying()
    {
        return sourceElevator.isPlaying || sourceDoor2.isPlaying || sourceDoor1.isPlaying;
    }
}
