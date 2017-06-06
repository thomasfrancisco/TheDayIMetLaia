using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAVoice3 : MonoBehaviour {

    public AudioClip n31_1;
    public AudioClip n31_2;
    public AudioClip n31_3;
    public AudioClip n31_4;
    public AudioClip n31_5_1;
    public AudioClip n31_5_2;
    public AudioClip n31_5_3;
    public AudioClip n31_6;
    public AudioClip n31_7;
    public AudioClip n31_8;

    public Transform trigEngineRoom;
    public Transform trigBrigde;
    public Transform puzzleSequence1;
    public Transform puzzleSequence2;
    public Transform trigMaintenance;
    public Transform trigControlRoom;
    public Transform trigExit;
    public Transform switchConsole1;
    public Transform switchConsole2;
    public Transform sourceEntry;
    public Transform sourceConsole1;
    public Transform sourceConsole2;
    public Transform sourceBridge;
    public Transform sourceEntryControlRoom;
    public Transform sourceEntryMaintenance;
    public Transform sourceExit;

    private SoundTemplate sound31_1;
    private SoundTemplate sound31_2;
    private SoundTemplate sound31_3;
    private SoundTemplate sound31_4;
    private SoundTemplate sound31_5_1;
    private SoundTemplate sound31_5_2;
    private SoundTemplate sound31_5_3;
    private SoundTemplate sound31_6;
    private SoundTemplate sound31_7;
    private SoundTemplate sound31_8;

    private TriggerIA triggerBridge;
    private Puzzle2Script puzzleSequence1Script;
    private Puzzle2Script puzzleSequence2Script;
    private TriggerIA triggerMaintenance;
    private TriggerIA triggerControlRoom;
    private TriggerIA triggerExit;
    private SwitchBehavior switchConsole1Script;
    private SwitchBehavior switchConsole2Script;
    private AudioSource sEntry;
    private AudioSource sConsole1;
    private AudioSource sConsole2;
    private AudioSource sBridge;
    private AudioSource sEntryControlRoom;
    private AudioSource sEntryMaintenance;
    private AudioSource sExit;

    private Transform ugo;

    private void Awake()
    {
        ugo = transform.Find("Player");
        triggerBridge = trigBrigde.GetComponent<TriggerIA>();
        puzzleSequence1Script = puzzleSequence1.GetComponent<Puzzle2Script>();
        puzzleSequence2Script = puzzleSequence2.GetComponent<Puzzle2Script>();
        triggerMaintenance = trigMaintenance.GetComponent<TriggerIA>();
        triggerControlRoom = trigControlRoom.GetComponent<TriggerIA>();
        triggerExit = trigExit.GetComponent<TriggerIA>();
        switchConsole1Script = switchConsole1.GetComponent<SwitchBehavior>();
        switchConsole2Script = switchConsole2.GetComponent<SwitchBehavior>();
        sEntry = sourceEntry.GetComponent<AudioSource>();
        sConsole1 = sourceConsole1.GetComponent<AudioSource>();
        sConsole2 = sourceConsole2.GetComponent<AudioSource>();
        sBridge = sourceBridge.GetComponent<AudioSource>();
        sEntryControlRoom = sourceEntryControlRoom.GetComponent<AudioSource>();
        sEntryMaintenance = sourceEntryMaintenance.GetComponent<AudioSource>();
        sExit = sourceExit.GetComponent<AudioSource>();
        sound31_1 = new SoundTemplate(n31_1, sEntry);
        sound31_2 = new SoundTemplate(n31_2, sConsole1);
        sound31_4 = new SoundTemplate(n31_4, sConsole2);
        sound31_5_1 = new SoundTemplate(n31_5_1, sBridge);
        sound31_5_2 = new SoundTemplate(n31_5_2, sBridge);
        sound31_5_3 = new SoundTemplate(n31_5_3, sBridge);
        sound31_6 = new SoundTemplate(n31_6, sEntryControlRoom);
        sound31_7 = new SoundTemplate(n31_7, sEntryMaintenance);
        sound31_8 = new SoundTemplate(n31_8, sExit);

    }

    // Update is called once per frame
    void Update () {
        if (Vector3.Distance(ugo.position, trigEngineRoom.position) < 1f && !sound31_1.isPlayed())
        {
            playSound(sound31_1);
        }

        if(Vector3.Distance(ugo.position, trigBrigde.position) < triggerBridge.distanceArea)
        {
            if (!sound31_5_1.isPlayed())
            {
                playSound(sound31_5_1);
            } else if (!sound31_5_3.isPlayed() && puzzleSequence1Script.unlocked && puzzleSequence2Script.unlocked)
            {
                playSound(sound31_5_3);
            } else if (!sound31_5_2.isPlayed() && (puzzleSequence1Script.unlocked || puzzleSequence2Script.unlocked))
            {
                playSound(sound31_5_2);
            }
        } else
        {
            sound31_5_1.setIsPlayed(false);
            sound31_5_2.setIsPlayed(false);
            sound31_5_3.setIsPlayed(false);
        }

        if(Vector3.Distance(ugo.position, trigMaintenance.position) < triggerMaintenance.distanceArea)
        {
            if (!sound31_7.isPlayed())
            {
                playSound(sound31_7);
            }
        } else
        {
            sound31_7.setIsPlayed(false);
        }

        if(Vector3.Distance(ugo.position, trigControlRoom.position) < triggerControlRoom.distanceArea)
        {

            if (!sound31_7.isPlayed())
            {
                playSound(sound31_7);
            }
        } else
        {
            sound31_7.setIsPlayed(false);
           
        }

        if(Vector3.Distance(ugo.position, trigExit.position) < triggerExit.distanceArea)
        {
            if (!sound31_8.isPlayed())
            {
                playSound(sound31_8);
            }
        }

        if(Vector3.Distance(ugo.position, switchConsole1.position) < switchConsole1Script.minDistTrigger && getAngleWithObject(switchConsole1) < switchConsole1Script.minAngle
            && (Input.GetButtonDown("Fire1") || Input.inputString == "\n"))
        {
            if (!sound31_2.isPlayed())
            {
                playSound(sound31_2);
            }
        }

        if(Vector3.Distance(ugo.position, switchConsole2.position) < switchConsole2Script.minDistTrigger && getAngleWithObject(switchConsole2) < switchConsole2Script.minAngle
            && (Input.GetButtonDown("Fire1") || Input.inputString == "\n"))
        {
            if (!sound31_4.isPlayed())
            {
                playSound(sound31_4);
            }
        }

        if (sound31_2.isFinished())
        {
            sound31_2.setIsPlayed(false);
        }
        if (sound31_4.isFinished())
        {
            sound31_4.setIsPlayed(false);
        }







	}

    private float getAngleWithObject(Transform target)
    {
        return Vector3.Angle(target.position - Camera.main.transform.position, Camera.main.transform.forward);
    }

    private void playSound(SoundTemplate sound)
    {
        sound.play();
        StartCoroutine(sound.endOfClip(0f));
    }


}
