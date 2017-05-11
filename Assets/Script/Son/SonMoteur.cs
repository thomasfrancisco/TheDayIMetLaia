using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonMoteur : MonoBehaviour {

    private enum UgoState { forward, backward, none};

    public AudioClip moveForwardStart;
    public AudioClip moveForwardLoop;
    public AudioClip moveForwardEnd;

    public AudioClip moveBackwardStart;
    public AudioClip moveBackwardLoop;
    public AudioClip moveBackwardEnd;

    
    public string currentlyPlaying;

    private AudioSource source;
    private RailMovementV2 UgoMovementScript;


    private UgoState ugoState;
    private bool lastUgoForwardState;
    private bool lastUgoBackwardState;
    private float timeLoop;
    private float minTimeToEnd = 0.5f; //Temps minimum de loop requis pour déclencher le son de fin 

    private void Awake()
    {
        timeLoop = 0f;
        UgoMovementScript = GetComponentInParent<RailMovementV2>();
        source = GetComponent<AudioSource>();
        lastUgoForwardState = false;
        lastUgoBackwardState = false;
        ugoState = UgoState.none;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        
        forwardSound();
        backwardSound();

        lastUgoForwardState = UgoMovementScript.isMovingForward;
        lastUgoBackwardState = UgoMovementScript.isMovingBackward;
	}

    //S'enclenche lorsque Ugo se déplace vers l'avant
    void forwardSound()
    {
        if (UgoMovementScript.isMovingForward && !lastUgoForwardState)
        {
            ugoState = UgoState.forward;
            //Debut
            source.clip = moveForwardStart;
            currentlyPlaying = "MoveForwardStart";
            source.loop = false;
            source.Play();
            timeLoop = 0f;
        }

        else if (UgoMovementScript.isMovingForward && lastUgoForwardState && ugoState == UgoState.forward)
        {
            timeLoop += Time.deltaTime;
            //Loop
            if (source.clip == moveForwardStart)
            {
                //On laisse le temps de se finir
                if (source.timeSamples == source.clip.samples)
                {
                    //MoveForwardStart fini
                    source.clip = moveForwardLoop;
                    currentlyPlaying = "MoveForwardLoop";
                    source.loop = true;
                    source.Play();
                }
            }
        }

        else if (!UgoMovementScript.isMovingForward && !lastUgoForwardState && ugoState == UgoState.forward)
        {
            //Fin
            if (source.clip == moveForwardStart || source.clip == moveForwardLoop)
            {
                if (timeLoop > minTimeToEnd)
                {
                    source.clip = moveForwardEnd;
                    currentlyPlaying = "MoveForwardEnd";
                    source.loop = false;
                    source.Play();
                    ugoState = UgoState.none;
                } else
                {
                    source.Stop();
                }
            }
        }
    }


    //S'enclenche lorsqu'Ugo se déplace vers l'arrier
    void backwardSound()
    {
        if(UgoMovementScript.isMovingBackward && !lastUgoBackwardState)
        {
            ugoState = UgoState.backward;
            //Debut
            source.clip = moveBackwardStart;
            currentlyPlaying = "MoveBackwardStart";
            source.loop = false;
            source.Play();
            timeLoop = 0f;

        } else if(UgoMovementScript.isMovingBackward && lastUgoBackwardState && ugoState == UgoState.backward)
        {
            //Loop
            timeLoop += Time.deltaTime;
            if(source.clip == moveBackwardStart)
            {
                //Laisse finir
                if(source.timeSamples == source.clip.samples)
                {
                    source.clip = moveBackwardLoop;
                    currentlyPlaying = "MoveBackwardLoop";
                    source.loop = true;
                    source.Play();
                }
            }

        } else if(!UgoMovementScript.isMovingBackward && !lastUgoBackwardState && ugoState == UgoState.backward)
        {
            //End
            if(source.clip == moveBackwardStart || source.clip == moveBackwardLoop)
            {
                if(timeLoop > minTimeToEnd)
                {
                    source.clip = moveBackwardEnd;
                    currentlyPlaying = "MoveBackwardEnd";
                    source.loop = false;
                    source.Play();
                    ugoState = UgoState.none;
                } else
                {
                    source.Stop();
                }
            }
        }
    }
    
}
