using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonMoteur : MonoBehaviour {

    public enum UgoState { forward, backward, none, blocked};

    public AudioClip moveForwardStart;
    public AudioClip moveForwardLoop;
    public AudioClip moveForwardEnd;

    public AudioClip moveBackwardStart;
    public AudioClip moveBackwardLoop;
    public AudioClip moveBackwardEnd;

    public AudioClip moveBlockedStart;
    public AudioClip moveBlockedLoop;
    public AudioClip moveBlockedEnd;
        
    //Debug
    public string DEBUGCurrentlyPlaying;
    public string DEBUGSample;

    private AudioSource source;
    private RailMovementV2 UgoMovementScript;


    public UgoState ugoState;
    public bool lastUgoForwardState;
    public bool lastUgoBackwardState;
    public bool lastUgoBlockedState;
    public float timeLoop;
    public float minTimeToEnd = 0.5f; //Temps minimum de loop requis pour déclencher le son de fin 

    private void Awake()
    {
        timeLoop = 0f;
        UgoMovementScript = GetComponentInParent<RailMovementV2>();
        source = GetComponent<AudioSource>();
        lastUgoForwardState = false;
        lastUgoBackwardState = false;
        lastUgoBlockedState = false;
        ugoState = UgoState.none;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        blockedSound();
        forwardSound();
        backwardSound();
        
        // DEBUG
        if (source.clip != null)
        {
            DEBUGCurrentlyPlaying = source.clip.name;
            DEBUGSample = source.timeSamples + "/" + source.clip.samples;
        }
        

        lastUgoForwardState = UgoMovementScript.isMovingForward;
        lastUgoBackwardState = UgoMovementScript.isMovingBackward;
        lastUgoBlockedState = UgoMovementScript.isMovingBlocked;
        
        
	}
    

    //S'enclenche lorsque Ugo se déplace vers l'avant
    void forwardSound()
    {
        if (UgoMovementScript.isMovingForward && !lastUgoForwardState)
        {
            ugoState = UgoState.forward;
            //Debut
            source.clip = moveForwardStart;
            source.loop = false;
            source.Play();
            timeLoop = 0f;
            StartCoroutine(forwardLoop());
        }
        /*
        else if (UgoMovementScript.isMovingForward && lastUgoForwardState && ugoState == UgoState.forward)
        {
            timeLoop += Time.deltaTime;
            //Loop
            if (source.clip == moveForwardStart)
            {
                //On laisse le temps de se finir
                if (source.timeSamples > source.clip.samples -5)
                {
                    //MoveForwardStart fini
                    source.clip = moveForwardLoop;
                    source.loop = true;
                    source.Play();
                }
            }
        }
        */
        else if (!UgoMovementScript.isMovingForward && !lastUgoForwardState && ugoState == UgoState.forward)
        {
            //Fin
            if (source.clip == moveForwardStart || source.clip == moveForwardLoop)
            {
                //if (timeLoop > minTimeToEnd)
                //{
                    source.clip = moveForwardEnd;
                    source.loop = false;
                    source.Play();
                    ugoState = UgoState.none;
                //} else
                //{
                //    source.Stop();
                //}
            }
        }
    }

    IEnumerator forwardLoop()
    {
        yield return new WaitForSeconds(moveForwardStart.length);
        if(UgoMovementScript.isMovingForward && lastUgoForwardState && ugoState == UgoState.forward)
        {
            source.clip = moveForwardLoop;
            source.loop = true;
            source.Play();
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
            source.loop = false;
            source.Play();
            timeLoop = 0f;
            StartCoroutine(backwardLoop());

        }
        /*else if(UgoMovementScript.isMovingBackward && lastUgoBackwardState && ugoState == UgoState.backward)
        {
            //Loop
            timeLoop += Time.deltaTime;
            if(source.clip == moveBackwardStart)
            {
                //Laisse finir
                if(source.timeSamples > source.clip.samples -5)
                {
                    source.clip = moveBackwardLoop;
                    source.loop = true;
                    source.Play();
                }
            }

        }
        */
        else if(!UgoMovementScript.isMovingBackward && !lastUgoBackwardState && ugoState == UgoState.backward)
        {
            //End
            if(source.clip == moveBackwardStart || source.clip == moveBackwardLoop)
            {
                //if(timeLoop > minTimeToEnd)
                //{
                    source.clip = moveBackwardEnd;
                    source.loop = false;
                    source.Play();
                    ugoState = UgoState.none;
                //} else
                //{
                //    source.Stop();
                //}
            }
        }
    }

    IEnumerator backwardLoop()
    {
        yield return new WaitForSeconds(moveBackwardStart.length);
        if(UgoMovementScript.isMovingBackward && lastUgoBackwardState && ugoState == UgoState.backward)
        {
            source.clip = moveBackwardLoop;
            source.loop = true;
            source.Play();
        }
    }
    
    //S'enclenche lorsqu'Ugo est bloqué
    void blockedSound()
    {
        if ((UgoMovementScript.isMovingBlocked && lastUgoBlockedState && ugoState == UgoState.blocked)
            || (lastUgoBackwardState || lastUgoForwardState) && UgoMovementScript.isMovingBlocked)
        {
            //Loop
            ugoState = UgoState.blocked;
            timeLoop += Time.deltaTime;
            if ((source.clip == moveBlockedStart && source.timeSamples > source.clip.samples -5)
                || source.clip == moveBackwardLoop || source.clip == moveForwardLoop)
            {
                source.clip = moveBlockedLoop;
                source.loop = true;
                source.Play();
            }

        }
        else if (UgoMovementScript.isMovingBlocked && !lastUgoBlockedState)
        {
            ugoState = UgoState.blocked;
            //Debut
            source.clip = moveBlockedStart;
            source.loop = false;
            source.Play();
            timeLoop = 0f;

        }
        
        else if (!UgoMovementScript.isMovingBlocked && !lastUgoBlockedState && ugoState == UgoState.blocked)
        {
            //End
            if (source.clip == moveBlockedStart || source.clip == moveBlockedLoop)
            {
                if (timeLoop > minTimeToEnd)
                {
                    source.clip = moveBlockedEnd;
                    source.loop = false;
                    source.Play();
                    ugoState = UgoState.none;
                }
                else
                {
                    source.Stop();
                }
            }
        }
    }
}
