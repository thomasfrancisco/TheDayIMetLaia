using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonSpeaker : MonoBehaviour {

    private enum actionExpected
    {
        intro,
        moveHead1,
        moveHead2,
        moveForward,
        activateDoor
    }

    public AudioClip n0_001;
    public AudioClip n0_002;
    public AudioClip n0_003;
    public AudioClip n0_004;
    public AudioClip n0_005;
    public AudioClip n0_006;

    public Transform puzzle1;
    public Transform ugo;
    public float minAngleForHeadMovement;

    private AudioSource source;
    private actionExpected nextAction;
    private Quaternion previousHeadRotation;
    private Puzzle1Script puzzle1Script;
    private RailMovement ugoRailMovement;

	// Use this for initialization
	void Awake () {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = false;
        puzzle1Script = puzzle1.FindChild("Tilt").GetComponent<Puzzle1Script>();
        ugoRailMovement = ugo.GetComponent<RailMovement>();
	}
	
	// Update is called once per frame
	void Update () {

        switch (nextAction)
        {
            case actionExpected.intro:
                if (source.clip != n0_001)
                {
                    source.clip = n0_001;
                    source.Play();
                } else
                {
                    if(source.timeSamples >= n0_001.samples)
                    {
                        nextAction = actionExpected.moveHead1;
                    }
                }

                break;
            case actionExpected.moveHead1:
                if(Quaternion.Angle(Camera.main.transform.rotation, previousHeadRotation) > minAngleForHeadMovement
                    && source.clip != n0_002)
                {
                    source.clip = n0_002;
                    source.Play();
                }
                if(source.clip == n0_002)
                {
                    if(source.timeSamples >= n0_002.samples)
                    {
                        nextAction = actionExpected.moveHead2;
                    }
                }
                break;

            case actionExpected.moveHead2:
                if (Quaternion.Angle(Camera.main.transform.rotation, previousHeadRotation) > minAngleForHeadMovement
                    && source.clip != n0_003)
                {
                    source.clip = n0_003;
                    source.Play();
                }
                if (source.clip == n0_003)
                {
                    if (source.timeSamples >= n0_003.samples)
                    {
                        ugoRailMovement.avanceDebloque = true;
                        nextAction = actionExpected.moveForward;
                    }
                }
                break;

            case actionExpected.moveForward:
                if(Input.GetAxis("Vertical") > 0
                    && !(source.clip == n0_004 || source.clip == n0_005))
                {
                    source.clip = n0_004;
                    source.Play();
                }
                if(source.clip == n0_004)
                {
                    if(source.timeSamples >= n0_004.samples)
                    {
                        source.clip = n0_005;
                        source.Play();
                    }
                }
                if(source.clip == n0_005)
                {
                    if(source.timeSamples >= n0_005.samples)
                    {
                        nextAction = actionExpected.activateDoor;
                    }
                }
                break;

            case actionExpected.activateDoor:
                if (puzzle1Script.isDoorOpen && source.clip != n0_006)
                {
                    source.clip = n0_006;
                    source.Play();
                }
                break;
        }
        previousHeadRotation = Camera.main.transform.rotation;
		
	}
    
}
