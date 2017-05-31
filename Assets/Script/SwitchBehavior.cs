using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : MonoBehaviour
{

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
        if(linkPuzzle != null)
            script = linkPuzzle.GetComponent<Puzzle2Script>();
        source = GetComponent<AudioSource>();
        scriptMovement = ugo.GetComponent<RailMovementV2>();
        isHover = false;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(ugo.position, transform.position) < minDistTrigger)
        {
            if (getAngleWithObject(transform) < minAngle)
            {
                if (!isHover)
                {
                    source.clip = PUZ2_DoneSwitch_Hover;
                    source.Play();
                    isHover = true;
                }
                if (Input.GetButtonDown("Fire1") || Input.inputString == "\n")
                {
                    scriptMovement.doAction = false;
                    if (linkPuzzle != null)
                        StartCoroutine(script.playSequence());
                }

            }
            else
            {
                isHover = false;
                if (source.isPlaying)
                    source.Stop();
            }
        } else
        {
            isHover = false;
            if (source.isPlaying && source.clip == PUZ2_DoneSwitch_Hover)
                source.Stop();
        }
    }

    private float getAngleWithObject(Transform target)
    {
        return Vector3.Angle(target.position - Camera.main.transform.position, Camera.main.transform.forward);
    }

    public void playWin()
    {
        source.volume = 1f;
        source.clip = PUZ2_Win;
        source.Play();
        StartCoroutine(resetHover());
    }

    public void playFail()
    {
        source.volume = 1f;
        source.clip = PUZ2_Fail;
        source.Play();
        StartCoroutine(resetHover());
    }

    IEnumerator resetHover()
    {
        yield return new WaitForSeconds(1f);
        isHover = false;
        source.volume = .25f;
    }


}
