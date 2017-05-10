using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonMoteur : MonoBehaviour {

    public AudioClip moveForwardStart;
    public AudioClip moveForwardLoop;
    public AudioClip moveForwardEnd;

    private AudioSource source;
    private RailMovementV2 UgoMovementScript;

    private bool lastUgoForwardState;
    private float timeLoop;
    private float minTimeToEnd = 0.5f; //Temps minimum de loop requis pour déclencher le son de fin 

    private void Awake()
    {
        timeLoop = 0f;
        UgoMovementScript = GetComponentInParent<RailMovementV2>();
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
            timeLoop = 0f;
        }

         else if(UgoMovementScript.isMovingForward && lastUgoForwardState)
        {
            timeLoop += Time.deltaTime;
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

        else if(!UgoMovementScript.isMovingForward && !lastUgoForwardState)
        {
            //Fin
            if (source.clip == moveForwardStart || source.clip == moveForwardLoop)
            {
                if (timeLoop > minTimeToEnd)
                {
                    source.clip = moveForwardEnd;
                    source.loop = false;
                    source.Play();
                }
            }
        }

        lastUgoForwardState = UgoMovementScript.isMovingForward;
	}
    
}
