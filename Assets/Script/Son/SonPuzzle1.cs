using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonPuzzle1 : MonoBehaviour {

    public AudioClip activeZone;
    public AudioClip doorOpen;
    public AudioClip electricLoop;
    public AudioClip signalFail;
    public AudioClip signalWin;

    private AudioSource sourceDoor;
    private AudioSource sourceSignal;
    private AudioSource sourceFeedback;
    private AudioSource sourceZoneActive;

    private Puzzle1Script script;

    private bool stopPlayingSignal;

    private void Awake()
    {
        stopPlayingSignal = false;
        sourceDoor = transform.Find("Son Porte").GetComponent<AudioSource>();
        sourceSignal = transform.Find("Tilt/Signal Electrique").GetComponent<AudioSource>();
        sourceFeedback = transform.Find("Tilt/FeedBack").GetComponent<AudioSource>();
        sourceZoneActive = transform.Find("Tilt/Signal Electrique/SonZoneActive").GetComponent<AudioSource>();
        script = GetComponentInChildren<Puzzle1Script>();
    }
    // Use this for initialization
    void Start () {
        sourceSignal.clip = electricLoop;
        sourceSignal.loop = true;
        sourceSignal.playOnAwake = true;
        sourceSignal.Play();

        sourceDoor.loop = false;
        sourceDoor.playOnAwake = false;

        sourceZoneActive.clip = activeZone;
        sourceZoneActive.playOnAwake = false;
        sourceZoneActive.loop = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!stopPlayingSignal)
        {
            if (script.isOnDoor)
            {
                sourceZoneActive.Play();
            }
            else
            {
                sourceZoneActive.Stop();
            }
        }
	}

    public void playFail()
    {
        sourceFeedback.clip = signalFail;
        sourceFeedback.Play();
    }

    public void playWin()
    {
        sourceSignal.Stop();
        stopPlayingSignal = true;
        sourceFeedback.clip = signalWin;
        sourceFeedback.Play();
    }

    public void playDoorOpen()
    {
        sourceDoor.clip = doorOpen;
        sourceDoor.Play();
    }
}
