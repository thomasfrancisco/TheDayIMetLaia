using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Son_Niveau_0 : MonoBehaviour {

    public enum actionExpected
    {
        Intro,
        TurnLeft,
        TurnRight,
        Move,
        Wait,
        HitDoor,
        OpenDoor,
        Nothing
    }

    public AudioClip ComVX_N0_1_1;
    public AudioClip ComVX_N0_1_2;
    public AudioClip ComVX_N0_1_3;
    public AudioClip ComVX_N0_1_4;
    public AudioClip ComVX_N0_2_1;
    public AudioClip ComVX_N0_2_2;
    public AudioClip ComVX_N0_2_3;
    public AudioClip ComVX_N0_3_1;
    public AudioClip ComVX_N0_3_2;
    public AudioClip ComVX_N0_3_3;
    public AudioClip ComVX_N0_3_4;
    public AudioClip ComVX_N0_3_5;


    public Transform puzzle1;
    public Transform ugo;
    public Transform blockingRail;
    public float minAngleHead;
    public float timeBeforeRepeat;

    private AudioSource[] sources;
    public actionExpected nextAction;
    private DoorScript doorScript;
    private RailMovementV2 ugoRailMovement;
    private SonAiguillage sonAiguillage;
    private RailScriptV2 blockingRailScript;
    private SonChoc sonchoc;
    private float previousHeadRotationY;
    private float timer;
    private int hitCount;


    // Use this for initialization
    void Awake()
    {
        initialiserSources();
        doorScript = puzzle1.GetComponent<DoorScript>();
        ugoRailMovement = ugo.GetComponent<RailMovementV2>();
        sonAiguillage = ugo.Find("Aiguillage").GetComponent<SonAiguillage>();
        sonchoc = ugo.FindChild("Choc").GetComponent<SonChoc>();
        blockingRailScript = blockingRail.GetComponent<RailScriptV2>();
        timer = 0f;
        hitCount = 0;
    }


    private void initialiserSources()
    {
        sources = GetComponentsInChildren<AudioSource>();
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].playOnAwake = false;
            sources[i].loop = false;
        }
        
    }

    private void setClip(AudioClip clip, bool play = true)
    {
        
        for(int i=0; i < sources.Length; i++)
        {
            sources[i].clip = clip;
            if (play)
                sources[i].Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

        switch (nextAction)
        {
            case actionExpected.Intro:
                if (sources[0].clip != ComVX_N0_1_1)
                {
                    setClip(ComVX_N0_1_1);
                } else
                {
                    if(sources[0].timeSamples >= ComVX_N0_1_1.samples - 5)
                    {
                        nextAction = actionExpected.TurnLeft;
                        previousHeadRotationY = Camera.main.transform.rotation.y;
                    }
                }
                break;

            case actionExpected.TurnLeft:
                if(Camera.main.transform.rotation.y < previousHeadRotationY - minAngleHead
                    && sources[0].clip != ComVX_N0_1_3)
                {
                    setClip(ComVX_N0_1_3);
                }
                if(sources[0].clip == ComVX_N0_1_3)
                {
                    if(sources[0].timeSamples >= ComVX_N0_1_3.samples - 5)
                    {
                        nextAction = actionExpected.TurnRight;
                        previousHeadRotationY = Camera.main.transform.rotation.y;
                        timer = 0f;
                    }
                } else if(timer > timeBeforeRepeat && sources[0].clip != ComVX_N0_1_2)
                {
                    if(!sources[0].isPlaying)
                        setClip(ComVX_N0_1_2);
                }
                timer += Time.deltaTime;
                break;

            case actionExpected.TurnRight:
                if(Camera.main.transform.rotation.y > previousHeadRotationY + minAngleHead
                    && sources[0].clip != ComVX_N0_2_1)
                {
                    setClip(ComVX_N0_2_1);
                }

                if(sources[0].clip == ComVX_N0_2_1)
                {
                    if(sources[0].timeSamples >= ComVX_N0_2_1.samples - 5)
                    {
                        nextAction = actionExpected.Wait;
                        timer = 0f;
                    }
                } else if (timer > timeBeforeRepeat && sources[0].clip != ComVX_N0_1_4)
                {
                    if (!sources[0].isPlaying)
                        setClip(ComVX_N0_1_4);
                }
                timer += Time.deltaTime;
                break;

            case actionExpected.Wait:
                sonAiguillage.playActivation();
                StartCoroutine(wait(1f));
               
                break;

            case actionExpected.Move:
                if(Input.GetAxis("Vertical") != 0 && sources[0].clip != ComVX_N0_3_1)
                {
                    setClip(ComVX_N0_3_1);
                }
                if(sources[0].clip == ComVX_N0_3_1)
                {
                    if(sources[0].timeSamples >= ComVX_N0_3_1.samples - 5)
                    {
                        nextAction = actionExpected.HitDoor;
                        timer = 0f;
                    }
                } else if(timer > timeBeforeRepeat && sources[0].clip != ComVX_N0_2_3)
                {
                    if (!sources[0].isPlaying)
                        setClip(ComVX_N0_2_3);
                }
                timer += Time.deltaTime;
                break;

            case actionExpected.HitDoor:
                if (blockingRailScript.isCollided)
                {
                    setClip(ComVX_N0_3_2);
                    nextAction = actionExpected.OpenDoor;
                }
                break;


            case actionExpected.OpenDoor:
                if(doorScript.isHatchOpen && sources[0].clip == ComVX_N0_3_2)
                {
                    setClip(ComVX_N0_3_3);
                } else
                {
                    if (doorScript.isDoorOpen)
                    {
                        setClip(ComVX_N0_3_5);
                        nextAction = actionExpected.Nothing;
                    } else
                    {
                        if(Input.GetButtonDown("Fire1") || Input.inputString == "\n")
                        {
                            hitCount++;
                            if(hitCount > 2 && sources[0].clip != ComVX_N0_3_4)
                            {
                                setClip(ComVX_N0_3_4);
                            }
                        }
                    }
                }
                break;


            case actionExpected.Nothing:
                break;
                /*
                 * Faut finir la porte avant d'attaquer ça
            case actionExpected.OpenDoor:
                if(sources[0].clip == ComVX_N0_3_2)
                {
                    if(Input.GetButtonDown("Fire1") || Input.inputString == "\n")
                    {
                        //
                    }
                }
                break;
                */

            /*

                break;
            case actionExpected.moveHead1:
                if (Quaternion.Angle(previousHeadRotation, Camera.main.transform.rotation) > minAngleForHeadMovement
                    && source.clip != n0_002)
                {
                    source.clip = n0_002;
                    source.Play();
                }
                if (source.clip == n0_002)
                {
                    if (source.timeSamples >= n0_002.samples)
                    {
                        nextAction = actionExpected.moveHead2;
                        previousHeadRotation = Camera.main.transform.rotation;
                    }
                }
                break;

            case actionExpected.moveHead2:
                if (Quaternion.Angle(previousHeadRotation, Camera.main.transform.rotation) > minAngleForHeadMovement
                    && source.clip != n0_003)
                {
                    source.clip = n0_003;
                    source.Play();
                }
                if (source.clip == n0_003)
                {
                    if (source.timeSamples >= n0_003.samples)
                    {
                        sonAiguillage.playActivation();
                        ugoRailMovement.avanceDebloque = true;
                        nextAction = actionExpected.moveForward;
                    }
                }
                break;

            case actionExpected.moveForward:
                if (Input.GetAxis("Vertical") > 0
                    && !(source.clip == n0_004 || source.clip == n0_005))
                {
                    source.clip = n0_004;
                    source.Play();
                }
                if (source.clip == n0_004)
                {
                    if (source.timeSamples >= n0_004.samples)
                    {
                        source.clip = n0_005;
                        source.Play();
                    }
                }
                if (source.clip == n0_005)
                {
                    if (source.timeSamples >= n0_005.samples)
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
        */
        }

    }

    IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        setClip(ComVX_N0_2_2);
        ugoRailMovement.avanceDebloque = true;
        ugoRailMovement.reculeDebloque = true;
        timer = 0f;
        nextAction = actionExpected.Move;
    }
    
}
