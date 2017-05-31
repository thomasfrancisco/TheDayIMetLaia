using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public Transform ugo;
    public float radiusDetection;
    public Transform relatedBlockedRail;
    public Transform nextRail;

    private RailScriptV2 relatedScript;
    private RailScriptV2 nextScript;

    private Animator tiltAnim;
    private Animator doorAnim;

    [HideInInspector]
    public bool isOnDoor;
    [HideInInspector]
    public bool isDoorOpen;
    public bool isHatchOpen;

    public AudioClip activeZone;
    public AudioClip doorOpen;
    public AudioClip electricLoop;
    public AudioClip signalFail;
    public AudioClip signalWin;

    private AudioSource sourceDoor;
    private AudioSource sourceSignal;
    private AudioSource sourceFeedback;

    private RailMovementV2 ugoMovement;

    private void Awake()
    {
        tiltAnim = transform.FindChild("Tilt").GetComponent<Animator>();
        doorAnim = GetComponent<Animator>();
        relatedScript = relatedBlockedRail.GetComponent<RailScriptV2>();
        nextScript = nextRail.GetComponent<RailScriptV2>();
        sourceDoor = transform.Find("Son Porte").GetComponent<AudioSource>();
        sourceSignal = transform.Find("Tilt/Signal Electrique").GetComponent<AudioSource>();
        sourceFeedback = transform.Find("Tilt/FeedBack").GetComponent<AudioSource>();
        ugoMovement = ugo.GetComponent<RailMovementV2>();
    }

    // Use this for initialization
    void Start () {
        isDoorOpen = false;
        isOnDoor = false;
        isHatchOpen = false;

        sourceDoor.loop = false;
        sourceDoor.playOnAwake = false;
        
        sourceSignal.loop = true;
        sourceSignal.playOnAwake = false;

        sourceFeedback.loop = false;
        sourceFeedback.playOnAwake = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(transform.position, ugo.position) < radiusDetection)
        {
            if(Input.inputString == "\n" || Input.GetButtonDown("Fire1"))
            {
                if (isHatchOpen)
                {
                    if (isOnDoor)
                    {
                        ugoMovement.doAction = false;
                        openingDoor();
                    } else
                    {
                        ugoMovement.doAction = false;
                        sourceFeedback.clip = signalFail;
                        sourceFeedback.Play();
                    }
                } else
                {
                    isHatchOpen = true;
                    ugoMovement.doAction = false;
                }
            }
        }

        if (isHatchOpen)
        {
            if (isOnDoor)
            {
                if (sourceSignal.clip != activeZone)
                {
                    sourceSignal.clip = activeZone;
                    sourceSignal.Play();
                }
            } else
            {
                if (sourceSignal.clip != electricLoop)
                {
                    sourceSignal.clip = electricLoop;
                    sourceSignal.Play();
                }
            }
        }
		
	}

    public void closeDoor()
    {
        doorAnim.SetBool("isOpen?", false);
    }

    private void openingDoor()
    {
        sourceSignal.Stop();
        isDoorOpen = true;
        sourceFeedback.clip = signalWin;
        sourceFeedback.Play();
        doorAnim.SetBool("isOpen?", true);
        tiltAnim.speed = 0;
        sourceDoor.clip = doorOpen;
        sourceDoor.Play();
        relatedScript.isBlocked = false;
        relatedScript.northRail = nextRail;
        nextScript.southRail = relatedBlockedRail;
        relatedScript.oneWay = true;
        nextScript.connectRails();
        relatedScript.connectRails();
    }
    
}
