using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : MonoBehaviour
{

    public float minDistTrigger;
    public float minAngle;
    public float delayRepeatSignal;

    public Transform linkPuzzle;
    public AudioClip PUZ2_DoneSwitch_Hover;
    public AudioClip PUZ2_Push;
    public AudioClip PUZ2_Win;
    public AudioClip PUZ2_Fail;
    public AudioClip signal;

    private Transform ugo;
    private Puzzle2Script script;
    private RailMovementV2 scriptMovement;
    private AudioSource source;
    private bool isHover;
    private float timer;

    private void Awake()
    {
        ugo = transform.Find("/Player");
        if(linkPuzzle != null)
            script = linkPuzzle.GetComponent<Puzzle2Script>();
        source = GetComponent<AudioSource>();
        scriptMovement = ugo.GetComponent<RailMovementV2>();
        isHover = false;
        timer = 0f;
    }
    // Use this for initialization
    void Start()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, minDistTrigger);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Vector3.Distance(ugo.position, transform.position) < minDistTrigger)
        {
            if (getAngleWithObject(transform) < minAngle)
            {

                source.loop = true;
                if (!isHover)
                {
                    if (!source.isPlaying)
                    {
                        source.clip = PUZ2_DoneSwitch_Hover;
                        source.Play();
                        isHover = true;
                    }
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
                source.loop = false;
                if (timer > delayRepeatSignal)
                {
                    source.clip = signal;
                    source.Play();
                    timer = 0f;
                }
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
        Vector3 targetCameraVec = target.position - Camera.main.transform.position;
        Vector2 targetCamVec2 = new Vector2(targetCameraVec.x, targetCameraVec.z);
        Vector2 forward = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
        return Vector2.Angle(targetCamVec2, forward);
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
