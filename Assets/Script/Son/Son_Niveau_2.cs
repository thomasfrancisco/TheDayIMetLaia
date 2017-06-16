using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Son_Niveau_2 : MonoBehaviour
{

    public AudioClip N2_0_1;
    public AudioClip N2_1_1;
    public AudioClip N2_1_2;
    public AudioClip N2_1_3;
    public AudioClip N2_2_1;
    public AudioClip N2_2_2;
    public AudioClip N2_3_1;
    public AudioClip N2_3_2;
    public AudioClip N2_3_3;
    public AudioClip N2_3_4;
    public AudioClip N2_4_1;
    public AudioClip N2_4_2;
    public AudioClip N2_5_1;
    public AudioClip N2_5_2;
    public AudioClip N2_5_3;
    public AudioClip N2_5_4;
    public AudioClip N2_5_5;
    public AudioClip N2_6_1;
    public AudioClip N2_6_2_1;
    public AudioClip N2_6_2_2;
    public AudioClip N2_6_3;
    public AudioClip N2_7_1;
    public AudioClip N2_7_2_1;
    public AudioClip N2_7_2_2;
    public AudioClip N2_8_1_1;
    public AudioClip N2_8_1_2;
    public AudioClip N2_8_2_1;
    public AudioClip N2_8_2_2;
    public AudioClip N2_8_3;
    public AudioClip N2_8_4_1;
    public AudioClip N2_8_4_2;
    public AudioClip N2_8_5_1;
    public AudioClip N2_8_5_2;

    public List<Transform> previousThing;
    public List<Transform> nextThing;
    public Transform previousDoor;

    public Transform triggerClosingDoor;
    public Transform railGlasses;
    public Transform railObstacleGlassHouse1;
    public Transform trigger1GlassHouse2;
    public Transform trigger2GlassHouse2;
    public Transform audiologGlassHouse3;
    public Transform railCorpse;
    public Transform audiolog1GlassHouse5;
    public Transform audiolog2GlassHouse5;
    public Transform railObstacleExit;
    public Transform relatedRailObstacle;
    public Transform railElevator;
    public Transform switchElevator;
    public Transform triggerPuzzleRoom;
    public Transform railPuzzleSequence;
    public Transform puzzleSequence;
    public Transform nextRailElevator;
    public Transform sfxBestiole1;
    public Transform sfxBestiole2;
    public Transform elevator;
    public Transform iaVoice;
    

    public float timeBeforeAudiologReminder;

    private SoundTemplate sound2_0_1;
    private SoundTemplate sound2_1_1;
    private SoundTemplate sound2_1_2;
    private SoundTemplate sound2_1_3;
    private SoundTemplate sound2_2_1;
    private SoundTemplate sound2_2_2;
    private SoundTemplate sound2_3_1;
    private SoundTemplate sound2_3_2;
    private SoundTemplate sound2_3_3;
    private SoundTemplate sound2_3_4;
    private SoundTemplate sound2_4_1;
    private SoundTemplate sound2_4_2;
    private SoundTemplate sound2_5_1;
    private SoundTemplate sound2_5_2;
    private SoundTemplate sound2_5_3;
    private SoundTemplate sound2_5_4;
    private SoundTemplate sound2_5_5;
    private SoundTemplate sound2_6_1;
    private SoundTemplate sound2_6_2_1;
    private SoundTemplate sound2_6_2_2;
    private SoundTemplate sound2_6_3;
    private SoundTemplate sound2_7_1;
    private SoundTemplate sound2_7_2_1;
    private SoundTemplate sound2_7_2_2;
    private SoundTemplate sound2_8_1_1;
    private SoundTemplate sound2_8_1_2;
    private SoundTemplate sound2_8_2_1;
    private SoundTemplate sound2_8_2_2;
    private SoundTemplate sound2_8_3;
    private SoundTemplate sound2_8_4_1;
    private SoundTemplate sound2_8_4_2;
    private SoundTemplate sound2_8_5_1;
    private SoundTemplate sound2_8_5_2;

    private DoorScript previousDoorScript;

    private RailScriptV2 triggerClosingDoorScript;
    private RailScriptV2 railGlassesScript;
    private RailScriptV2 railObstacleGlassHouse1Script;
    private AudioLogBehaviour audiologGlassHouse3Script;
    private RailScriptV2 railCorpseScript;
    private AudioLogBehaviour audiolog1GlassHouse5Script;
    private AudioLogBehaviour audiolog2GlassHouse5Script;
    private RailScriptV2 railObstacleExitScript;
    private RailScriptV2 railElevatorScript;
    private SwitchBehavior switchElevatorScript;
    private Puzzle2Script puzzleSequenceScript;
    private RailScriptV2 railPuzzleSequenceScript;
    private RailScriptV2 nextRailElevatorScript;
    private sfxSound sfxBestiole1Script;
    private sfxSound sfxBestiole2Script;
    private ElevatorSound elevatorScript;
    private IAVoice2 iaVoiceScript;

    private Transform ugo;
    private RailMovementV2 ugoMovement;

    private bool isDoorSeen;
    private bool isElevatorSeen;


    private List<AudioSource> sources;

    private void Awake()
    {
        sources = new List<AudioSource>(GetComponentsInChildren<AudioSource>());
        sound2_0_1 = new SoundTemplate(N2_0_1, sources);
        sound2_1_1 = new SoundTemplate(N2_1_1, sources);
        sound2_1_2 = new SoundTemplate(N2_1_2, sources);
        sound2_1_3 = new SoundTemplate(N2_1_3, sources);
        sound2_2_1 = new SoundTemplate(N2_2_1, sources);
        sound2_2_2 = new SoundTemplate(N2_2_2, sources);
        sound2_3_1 = new SoundTemplate(N2_3_1, sources);
        sound2_3_2 = new SoundTemplate(N2_3_2, sources);
        sound2_3_3 = new SoundTemplate(N2_3_3, sources);
        sound2_3_4 = new SoundTemplate(N2_3_4, sources);
        sound2_4_1 = new SoundTemplate(N2_4_1, sources);
        sound2_4_2 = new SoundTemplate(N2_4_2, sources);
        sound2_5_1 = new SoundTemplate(N2_5_1, sources);
        sound2_5_2 = new SoundTemplate(N2_5_2, sources);
        sound2_5_3 = new SoundTemplate(N2_5_3, sources);
        sound2_5_4 = new SoundTemplate(N2_5_4, sources);
        sound2_5_5 = new SoundTemplate(N2_5_5, sources);
        sound2_6_1 = new SoundTemplate(N2_6_1, sources);
        sound2_6_2_1 = new SoundTemplate(N2_6_2_1, sources);
        sound2_6_2_2 = new SoundTemplate(N2_6_2_2, sources);
        sound2_6_3 = new SoundTemplate(N2_6_3, sources);
        sound2_7_1 = new SoundTemplate(N2_7_1, sources);
        sound2_7_2_1 = new SoundTemplate(N2_7_2_1, sources);
        sound2_7_2_2 = new SoundTemplate(N2_7_2_2, sources);
        sound2_8_1_1 = new SoundTemplate(N2_8_1_1, sources);
        sound2_8_1_2 = new SoundTemplate(N2_8_1_2, sources);
        sound2_8_2_1 = new SoundTemplate(N2_8_2_1, sources);
        sound2_8_2_2 = new SoundTemplate(N2_8_2_2, sources);
        sound2_8_3 = new SoundTemplate(N2_8_3, sources);
        sound2_8_4_1 = new SoundTemplate(N2_8_4_1, sources);
        sound2_8_4_2 = new SoundTemplate(N2_8_4_2, sources);
        sound2_8_5_1 = new SoundTemplate(N2_8_5_1, sources);
        sound2_8_5_2 = new SoundTemplate(N2_8_5_2, sources);

        previousDoorScript = previousDoor.GetComponent<DoorScript>();
        triggerClosingDoorScript = triggerClosingDoor.GetComponent<RailScriptV2>();
        railGlassesScript = railGlasses.GetComponent<RailScriptV2>();
        railObstacleGlassHouse1Script = railObstacleGlassHouse1.GetComponent<RailScriptV2>();
        audiologGlassHouse3Script = audiologGlassHouse3.GetComponent<AudioLogBehaviour>();
        railCorpseScript = railCorpse.GetComponent<RailScriptV2>();
        audiolog1GlassHouse5Script = audiolog1GlassHouse5.GetComponent<AudioLogBehaviour>();
        audiolog2GlassHouse5Script = audiolog2GlassHouse5.GetComponent<AudioLogBehaviour>();
        railObstacleExitScript = railObstacleExit.GetComponent<RailScriptV2>();
        railElevatorScript = railElevator.GetComponent<RailScriptV2>();
        switchElevatorScript = switchElevator.GetComponent<SwitchBehavior>();
        puzzleSequenceScript = puzzleSequence.GetComponent<Puzzle2Script>();
        railPuzzleSequenceScript = railPuzzleSequence.GetComponent<RailScriptV2>();
        nextRailElevatorScript = nextRailElevator.GetComponent<RailScriptV2>();
        sfxBestiole1Script = sfxBestiole1.GetComponent<sfxSound>();
        sfxBestiole2Script = sfxBestiole2.GetComponent<sfxSound>();
        elevatorScript = elevator.GetComponent<ElevatorSound>();
        iaVoiceScript = iaVoice.GetComponent<IAVoice2>();

        ugo = transform.Find("/Player");
        ugoMovement = ugo.GetComponent<RailMovementV2>();
        isDoorSeen = false;
        isElevatorSeen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ugo.position.z > triggerClosingDoor.position.z && !sound2_0_1.isPlayed())
        {
            if (!iaVoiceScript.isPlaying())
            {
                playSound(sound2_0_1);


                foreach (Transform thing in previousThing)
                {
                    thing.gameObject.SetActive(false);
                }
                previousDoorScript.closeDoor();
                triggerClosingDoorScript.southRail = null;
                triggerClosingDoorScript.oneWay = false;
                triggerClosingDoorScript.isBlocked = true;
                triggerClosingDoorScript.connectRails();
            }
        }
        if (Vector3.Distance(ugo.position, railGlasses.position) < 1f && ugo.position.x < railGlasses.position.x
                   && !sound2_1_1.isPlayed())
        {
            playSound(sound2_1_1);

        }
        if (ugoMovement.getIntersection() == railObstacleGlassHouse1Script && !sound2_1_2.isPlayed())
        {
            playSound(sound2_1_2);

        }
        if (ugo.position.x > railObstacleGlassHouse1.position.x && ugo.position.x < railGlasses.position.x
                   && ugo.eulerAngles.y > -70f && ugo.eulerAngles.y < -110f && !sound2_1_3.isPlayed())
        {
            playSound(sound2_1_3);

        }
        if (Vector3.Distance(ugo.position, trigger1GlassHouse2.position) < 1f && !sound2_2_1.isPlayed())
        {
            playSound(sound2_2_1);

        }
        if (Vector3.Distance(ugo.position, trigger2GlassHouse2.position) < 1f && !sound2_2_2.isPlayed())
        {
            playSound(sound2_2_2);

        }
        if (Vector3.Distance(ugo.position, audiologGlassHouse3.position) < audiologGlassHouse3Script.trigger_dist
                   && !sound2_3_1.isPlayed())
        {
            playSound(sound2_3_1);

            StartCoroutine(testAudiologPlayed());
        }
        if (audiologGlassHouse3Script.isFinished && !sound2_3_3.isPlayed())
        {
            playSound(sound2_3_3);

        }
        if (Vector3.Distance(ugo.position, audiologGlassHouse3.position) > audiologGlassHouse3Script.trigger_dist
                   && sound2_3_1.isPlayed() && audiologGlassHouse3Script.audioLog_Played  && !audiologGlassHouse3Script.isFinished && !sound2_3_4.isPlayed())
        {
            playSound(sound2_3_4);

        }
        if (ugoMovement.getIntersection() == railCorpseScript && !sound2_4_1.isPlayed())
        {
            playSound(sound2_4_1);
            StartCoroutine(playBestioles());

        }
        if (Vector3.Distance(ugo.position, audiolog1GlassHouse5.position) < audiolog1GlassHouse5Script.trigger_dist
                   && !audiolog1GlassHouse5Script.audioLog_Played && isDoorSeen && !sound2_5_2.isPlayed())
        {
            playSound(sound2_5_2);

        }
        if (audiolog1GlassHouse5Script.isFinished && !isDoorSeen && !sound2_5_1.isPlayed())
        {
            playSound(sound2_5_1);

        }
        if (Vector3.Distance(ugo.position, audiolog1GlassHouse5.position) < audiolog1GlassHouse5Script.trigger_dist && audiolog1GlassHouse5Script.isFinished && isDoorSeen && !sound2_5_3.isPlayed())
        {
            //Faudra penser a reset la valeur isFinished quand la porte sera vu
            playSound(sound2_5_3);

        }
        if (Vector3.Distance(ugo.position, audiolog2GlassHouse5.position) < audiolog2GlassHouse5Script.trigger_dist
                   && !audiolog2GlassHouse5Script.audioLog_Played && !sound2_5_4.isPlayed())
        {
            playSound(sound2_5_4);

        }
        if (audiolog2GlassHouse5Script.isFinished && !sound2_5_5.isPlayed())
        {
            playSound(sound2_5_5);

        }
        if (ugoMovement.getIntersection() == railObstacleExitScript && railObstacleExitScript.isBlocked
                   && !sound2_6_1.isPlayed())
        {
            playSound(sound2_6_1);

        }
        if (!railObstacleExitScript.isBlocked && sound2_6_1.isPlayed() && ugo.position.x == railObstacleExit.position.x
                   && Camera.main.transform.eulerAngles.y > -20f && Camera.main.transform.eulerAngles.y < 20f &&
                   !sound2_6_2_2.isPlayed() && !sound2_6_2_1.isPlayed())
        {
            playSound(sound2_6_2_1);

        }
        if (!railObstacleExitScript.isBlocked && !sound2_6_1.isPlayed() && ugo.position.x == railObstacleExit.position.x
                   && Camera.main.transform.eulerAngles.y > -20f && Camera.main.transform.eulerAngles.y < 20f
                   && !sound2_6_2_1.isPlayed() && !sound2_6_2_2.isPlayed())
        {
            playSound(sound2_6_2_2);

        }
        if ((sound2_6_2_1.isFinished() || sound2_6_2_2.isFinished()) && !sound2_6_3.isPlayed())
        {
            playSound(sound2_6_3);

        }
        if (ugoMovement.getIntersection() == railElevatorScript && !sound2_7_1.isPlayed())
        {
            playSound(sound2_7_1);


        }
        if (Vector3.Distance(ugo.position, switchElevator.position) < switchElevatorScript.minDistTrigger
                   && getAngleWithObject(switchElevator) < switchElevatorScript.minAngle
                   && (Input.GetButtonDown("Fire1") || Input.inputString == "\n"))
        {
            if (!puzzleSequenceScript.unlocked && !sound2_7_2_1.isPlayed())
            {
                isElevatorSeen = true;
                playSound(sound2_7_2_1);

            }
            if (puzzleSequenceScript.unlocked && !sound2_7_2_2.isPlayed())
            {                
                StartCoroutine(endOfLevel());

            }
        }
        if (Vector3.Distance(ugo.position, triggerPuzzleRoom.position) < 1f && ugo.position.x < triggerPuzzleRoom.position.x)
        {
            if (isElevatorSeen && !sound2_8_1_1.isPlayed())
            {
                playSound(sound2_8_1_1);

            }
            if (!isElevatorSeen && !sound2_8_1_2.isPlayed())
            {
                playSound(sound2_8_1_2);

            }
        }
        if (ugoMovement.getIntersection() == railPuzzleSequenceScript)
        {
            /**
             * Ce passage risque d'avoir des incohérences..
             * A corriger apres test
             * */
            if (!isElevatorSeen && !sound2_8_2_1.isPlayed())
            {
                playSound(sound2_8_2_1);

            }
            if (isElevatorSeen && !sound2_8_2_2.isPlayed())
            {
                playSound(sound2_8_2_2);

            }
            if (puzzleSequenceScript.unlocked && !sound2_8_3.isPlayed())
            {
                playSound(sound2_8_3);

            }
            if (!puzzleSequenceScript.unlocked && puzzleSequenceScript.getNbMissed() == 1
                           && !audiolog1GlassHouse5Script.audioLog_Played && !sound2_8_4_1.isPlayed())
            {
                playSound(sound2_8_4_1);

            }
            if (!puzzleSequenceScript.unlocked && puzzleSequenceScript.getNbMissed() == 1
                           && audiolog1GlassHouse5Script.audioLog_Played && !sound2_8_4_2.isPlayed())
            {
                playSound(sound2_8_4_2);

            }
            if (!puzzleSequenceScript.unlocked && puzzleSequenceScript.getNbMissed() > 3
                           && audiolog1GlassHouse5Script.audioLog_Played && !sound2_8_5_1.isPlayed())
            {
                playSound(sound2_8_5_1);

            }
            if (!puzzleSequenceScript.unlocked && puzzleSequenceScript.getNbMissed() > 8
                           && audiolog1GlassHouse5Script.audioLog_Played && !sound2_8_5_2.isPlayed())
            {
                playSound(sound2_8_5_2);

            }
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

    private IEnumerator testAudiologPlayed()
    {
        yield return new WaitForSeconds(timeBeforeAudiologReminder);
        if (!audiologGlassHouse3Script.audioLog_Played)
            playSound(sound2_3_2);
    }

    private IEnumerator endOfLevel()
    {
        elevatorScript.playElevator();
        railElevatorScript.westRail = null;
        railElevatorScript.connectRails();
        playSound(sound2_7_2_2);
        yield return new WaitForSeconds(N2_7_2_2.length + 2f);
        elevatorScript.stopElevator();

        foreach (Transform thing in nextThing)
        {
            thing.gameObject.SetActive(true);
        }
        railElevatorScript.eastRail = nextRailElevator;
        nextRailElevatorScript.westRail = railElevator;
        railElevatorScript.connectRails();
        nextRailElevatorScript.connectRails();

    }

    private IEnumerator playBestioles()
    {
        yield return new WaitForSeconds(N2_4_1.length);
        sfxBestiole1Script.play();
        yield return new WaitForSeconds(sfxBestiole1Script.clip.length);
        sfxBestiole2Script.play();
        yield return new WaitForSeconds(sfxBestiole2Script.clip.length - 2f);
        playSound(sound2_4_2);

        railObstacleExitScript.northRail = relatedRailObstacle;
        railObstacleExitScript.isBlocked = false;
        railObstacleExitScript.oneWay = true;
        railObstacleExitScript.connectRails();

    }

    private void manageLevel()
    {
    }

}
