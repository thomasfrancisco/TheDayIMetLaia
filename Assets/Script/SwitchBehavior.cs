using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : MonoBehaviour {

    public float minDistTrigger;
    public float minAngle;

    public Transform linkPuzzle;
    public AudioClip PUZ2_DoneSwitch_Hover;
    public AudioClip PUZ2_Push;
    public AudioClip PUZ2_Win;
    public AudioClip PUZ2_Fail;

    private Transform ugo;
    private Puzzle2Script script;
    private RailMovementV2 scriptMovement;
    private AudioSource source;
    private bool isHover;

    private void Awake()
    {
        ugo = transform.Find("/Player");
        script = linkPuzzle.GetComponent<Puzzle2Script>();
        source = GetComponent<AudioSource>();
        scriptMovement = ugo.GetComponent<RailMovementV2>();
        isHover = false;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(ugo.position, transform.position) < minDistTrigger)
        {
            if(getAngleWithObject(transform) < minAngle)
            {
                if (!isHover)
                {
                    //source.clip = PUZ2_DoneSwitch_Hover;
                    //source.Play();
                    isHover = true;
                }
                if(Input.GetButtonDown("Fire1") || Input.inputString == "\n")
                {
                    // source.clip = push
                    scriptMovement.doAction = false;
                    StartCoroutine(script.playSequence());
                }

            } else
            {
                isHover = false;
            }
        }		
	}

    private float getAngleWithObject(Transform target)
    {
        return Vector3.Angle(target.position - Camera.main.transform.position, Camera.main.transform.forward);
    }

    public void playWin()
    {
        source.clip = PUZ2_Win;
        source.Play();
    }

    public void playFail()
    {
        source.clip = PUZ2_Fail;
        source.Play();
    }
    

}
