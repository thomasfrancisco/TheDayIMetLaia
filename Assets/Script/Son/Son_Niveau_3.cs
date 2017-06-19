using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Son_Niveau_3 : MonoBehaviour {

    public AudioClip N3_1;
    public AudioClip N32_1;
    public AudioClip N32_2;
    public AudioClip N32_3;
    public AudioClip N32_4;
    public AudioClip N32_5;
    public AudioClip N32_6_1;
    public AudioClip N32_6_1_2;
    public AudioClip N32_6_2;
    public AudioClip N32_6_3;
    public AudioClip N32_7;

    public List<Transform> previousThing;
    public List<Transform> nextThing;

    public Transform triggerRadioOff;
    public Transform puzzle1;
    public Transform puzzle2;
    public Transform railBridge1;
    public Transform railBridge2;
    public Transform railElevator;
    public Transform railNextElevator;
    public Transform elevator;
    public Transform switchElevator;
    public Transform triggerRadioOn;
    public Transform railDeadEnd;
    public Transform railFrontSas;
    public Transform railInSas;
    public Transform railFrontExit;
    public Transform doorSas;
    public Transform iaVoice4;
    public Transform iaVoice3;
    public Transform doorExit;
    public Transform sfxRailDesact;
    public Transform sfxDepress;
    public Transform sfxAlarme;
    public Transform sons_epilogue;
    public List<Transform> thingToHideInElevator;
    

    private SoundTemplate sound3_1;
    private SoundTemplate sound32_1;
    private SoundTemplate sound32_2;
    private SoundTemplate sound32_3;
    private SoundTemplate sound32_4;
    private SoundTemplate sound32_5;
    private SoundTemplate sound32_6_1;
    private SoundTemplate sound32_6_1_2;
    private SoundTemplate sound32_6_2;
    private SoundTemplate sound32_6_3;
    private SoundTemplate sound32_7;

    private Transform ugo;
    private RailMovementV2 ugoMovement;
    private Puzzle2Script puzzle1Script;
    private Puzzle2Script puzzle2Script;
    private RailScriptV2 railBridge1Script;
    private RailScriptV2 railBridge2Script;
    private RailScriptV2 railElevatorScript;
    private RailScriptV2 railNextElevatorScript;
    private ElevatorSound elevatorScript;
    private SwitchBehavior switchElevatorScript;
    private RailScriptV2 railInSasScript;
    private RailScriptV2 railFrontExitScript;
    private SimpleDoor doorSasScript;
    private IAVoice4 iaVoice;
    private IAVoice3 iaVoice3Script;
    private DoorScript doorExitScript;
    private AudioSource source;
    private sfxSound sfxRailDesactScript;
    private sfxSound sfxDepresScript;
    private sfxSound sfxAlarmeScript;
    private Son_Niveau_4 sons_epilogueScript;
    private SonAiguillage sonAiguillage;

    private bool isBridgeConnected;
    

    private void Awake()
    {
        ugo = transform.Find("/Player");
        ugoMovement = ugo.GetComponent<RailMovementV2>();
        sonAiguillage = ugo.FindChild("Aiguillage").GetComponent<SonAiguillage>();
        puzzle1Script = puzzle1.GetComponent<Puzzle2Script>();
        puzzle2Script = puzzle2.GetComponent<Puzzle2Script>();
        railBridge1Script = railBridge1.GetComponent<RailScriptV2>();
        railBridge2Script = railBridge2.GetComponent<RailScriptV2>();
        railElevatorScript = railElevator.GetComponent<RailScriptV2>();
        railNextElevatorScript = railNextElevator.GetComponent<RailScriptV2>();
        elevatorScript = elevator.GetComponent<ElevatorSound>();
        switchElevatorScript = switchElevator.GetComponent<SwitchBehavior>();
        doorSasScript = doorSas.GetComponent<SimpleDoor>();
        iaVoice = iaVoice4.GetComponent<IAVoice4>();
        iaVoice3Script = iaVoice3.GetComponent<IAVoice3>();
        doorExitScript = doorExit.GetComponent<DoorScript>();
        railInSasScript = railInSas.GetComponent<RailScriptV2>();
        railFrontExitScript = railFrontExit.GetComponent<RailScriptV2>();
        sfxRailDesactScript = sfxRailDesact.GetComponent<sfxSound>();
        sfxDepresScript = sfxDepress.GetComponent<sfxSound>();
        sfxAlarmeScript = sfxAlarme.GetComponent<sfxSound>();
        source = GetComponentInChildren<AudioSource>();
        sound32_1 = new SoundTemplate(N32_1, source);
        sound32_2 = new SoundTemplate(N32_2, source);
        sound32_3 = new SoundTemplate(N32_3, source);
        sound32_4 = new SoundTemplate(N32_4, source);
        sound32_5 = new SoundTemplate(N32_5, source);
        sound32_6_1 = new SoundTemplate(N32_6_1, source);
        sound32_6_1_2 = new SoundTemplate(N32_6_1_2, source);
        sound32_6_2 = new SoundTemplate(N32_6_2, source);
        sound32_6_3 = new SoundTemplate(N32_6_3, source);
        sound32_7 = new SoundTemplate(N32_7, source);
        sound3_1 = new SoundTemplate(N3_1, source);
        isBridgeConnected = false;
        sons_epilogueScript = sons_epilogue.GetComponent<Son_Niveau_4>();

    }


    // Update is called once per frame
    void Update () {
		if(ugo.position.x > triggerRadioOff.position.x && ugo.position.x < triggerRadioOff.position.x +50f && !sound3_1.isPlayed())
        {
            if (!iaVoice3Script.isPlaying())
            {
                playSound(sound3_1);
                foreach (Transform thing in previousThing)
                {
                    thing.gameObject.SetActive(false);
                }
                foreach (Transform thing in nextThing)
                {
                    thing.gameObject.SetActive(true);
                }
            }
        }


        if(Vector3.Distance(ugo.position, railDeadEnd.position) < 1f && !sound32_4.isPlayed())
        {
            playSound(sound32_4);
        }

        if(Vector3.Distance(ugo.position, railFrontSas.position) < 1f && !sound32_5.isPlayed())
        {
            playSound(sound32_5);
            doorSasScript.isLocked = false;
        }

        if(ugoMovement.getIntersection() == railInSasScript && !sound32_6_1.isPlayed())
        {
            railInSasScript.westRail = null;
            railInSasScript.connectRails();
            playSound(sound32_6_1);
            StartCoroutine(playSasDial());
        }

        if(puzzle1Script.unlocked && puzzle2Script.unlocked && !isBridgeConnected)
        {
            isBridgeConnected = true;
            railBridge1Script.eastRail = railBridge2;
            railBridge2Script.westRail = railBridge1;
            railBridge1Script.connectRails();
            railBridge2Script.connectRails();
        }

        if(Vector3.Distance(ugo.position, switchElevator.position) < switchElevatorScript.minDistTrigger
                   && getAngleWithObject(switchElevator) < switchElevatorScript.minAngle
                   && (Input.GetButtonDown("Fire1") || Input.inputString == "\n")
                   && !elevatorScript.isPlaying())
        {
            StartCoroutine(playElevator());
        }

        if (doorExitScript.isDoorOpen)
        {
            sons_epilogueScript.playFinal();
            doorExitScript.isDoorOpen = false;
        }
        
	}

    private float getAngleWithObject(Transform target)
    {
        Vector3 targetCameraVec = target.position - Camera.main.transform.position;
        Vector2 targetCamVec2 = new Vector2(targetCameraVec.x, targetCameraVec.z);
        Vector2 forward = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
        return Vector2.Angle(targetCamVec2, forward);
    }

    private void playSound(SoundTemplate sound)
    {
        sound.play();
        StartCoroutine(sound.endOfClip(0f));
    }

    private IEnumerator playDelayed(SoundTemplate sound, float time)
    {
        yield return new WaitForSeconds(time);
        playSound(sound);
    }
    
    private IEnumerator playElevator()
    {
        elevatorScript.playElevator();
        foreach(Transform thing in thingToHideInElevator)
        {
            thing.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(elevatorScript.start.length + 2f);
        playSound(sound32_1);
        StartCoroutine(playDelayed(sound32_2, N32_1.length + 3f));
        StartCoroutine(playDelayed(sound32_3, N32_1.length + N32_2.length + 3f));
        yield return new WaitForSeconds(N32_1.length + N32_2.length + N32_3.length);
        railElevatorScript.eastRail = railNextElevator;
        railElevatorScript.westRail = null;
        railElevatorScript.connectRails();
        elevatorScript.stopElevator();
    }

    private IEnumerator playSasDial()
    {
        yield return new WaitForSeconds(N32_6_1.length);
        doorSasScript.closeDoor();
        railInSasScript.westRail = null;
        railInSasScript.isBlocked = true;
        railInSasScript.connectRails();
        sfxRailDesactScript.play();
        yield return new WaitForSeconds(1f);
        playSound(sound32_6_1_2);
        yield return new WaitForSeconds(N32_6_1_2.length);
        iaVoice.playN32_2();
        sfxDepresScript.play();
        yield return new WaitForSeconds(iaVoice.n32_2.length + 5f);
        iaVoice.playN32_3();
        sfxAlarmeScript.getSource().loop = true;
        sfxAlarmeScript.play();
        yield return new WaitForSeconds(iaVoice.n32_3.length);
        playSound(sound32_6_2);
        yield return new WaitForSeconds(N32_6_2.length);
        iaVoice.playN32_4();
        sfxDepresScript.stop();
        yield return new WaitForSeconds(iaVoice.n32_4.length);
        playSound(sound32_6_3);
        yield return new WaitForSeconds(N32_6_3.length);
        railInSasScript.eastRail = railFrontExit;
        railFrontExitScript.westRail = railInSas;
        railInSasScript.connectRails();
        railFrontExitScript.connectRails();
        sonAiguillage.playIntersection();
        yield return new WaitForSeconds(10f);
        if (!doorExitScript.isDoorOpen)
        {
            playSound(sound32_7);
        }



    }
    
}
