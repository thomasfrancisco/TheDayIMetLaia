using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Son_Niveau_1 : MonoBehaviour {
    public AudioClip N1_1_1;
    public AudioClip N1_2_1;
    public AudioClip N1_2_2;
    public AudioClip N1_3_1;
    public AudioClip N1_3_2;
    public AudioClip N1_3_3;
    public AudioClip N1_3_4;
    public AudioClip N1_4_1;
    public AudioClip N1_4_2;
    public AudioClip N1_4_3;
    public AudioClip N1_5;
    public AudioClip N1_6_1;

    public Transform previousSpeakers;
    public Transform previousSoundAmb;
    public Transform previousLevel;
    public Transform firstDoor;
    public Transform railTriggerClosingDoor;
    public Transform railDouille;
    public Transform railObstacle;
    public Transform intersectionTutoDemitour;
    public Transform intersectionSansAig;
    public Transform railEvent01;
    public Transform railDoorClosed;
    public Transform iaVoice;

    public float delayBeforeUTurnReminder;

    public Transform nextSpeakers;
    public Transform nextRail;
    public Transform previousRail;

    private List<AudioSource> sources;
    private SoundTemplate sound1_1_1;
    private SoundTemplate sound1_2_1;
    private SoundTemplate sound1_2_2;
    private SoundTemplate sound1_3_1;
    private SoundTemplate sound1_3_2;
    private SoundTemplate sound1_3_3;
    private SoundTemplate sound1_3_4;
    private SoundTemplate sound1_4_1;
    private SoundTemplate sound1_4_2;
    private SoundTemplate sound1_4_3;
    private SoundTemplate sound1_5;
    private SoundTemplate sound1_6_1;

    private DoorScript firstDoorScript;
    private RailScriptV2 railTriggerClosingDoorScript;
    private RailScriptV2 railDouilleScript;
    private RailScriptV2 railObstacleScript;
    private RailScriptV2 intersectionTutoDemiTourScript;
    private RailScriptV2 intersectionSansAigScript;
    private RailScriptV2 railEvent01Script;
    private RailScriptV2 railDoorClosedScript;
    private IAVoice1 iaVoiceScript;

    private Transform ugo;
    private RailMovementV2 ugoMovement;

    //Permettant les trigger d'erreur du joueur
    private bool uTurnDone;
    

    private void Awake()
    {
        sources = new List<AudioSource>(GetComponentsInChildren<AudioSource>());
        sound1_1_1 = new SoundTemplate(N1_1_1, sources);
        sound1_2_1 = new SoundTemplate(N1_2_1, sources);
        sound1_2_2 = new SoundTemplate(N1_2_2, sources);
        sound1_3_1 = new SoundTemplate(N1_3_1, sources);
        sound1_3_2 = new SoundTemplate(N1_3_2, sources);
        sound1_3_3 = new SoundTemplate(N1_3_3, sources);
        sound1_3_4 = new SoundTemplate(N1_3_4, sources);
        sound1_4_1 = new SoundTemplate(N1_4_1, sources);
        sound1_4_2 = new SoundTemplate(N1_4_2, sources);
        sound1_4_3 = new SoundTemplate(N1_4_3, sources);
        sound1_5 = new SoundTemplate(N1_5, sources);
        sound1_6_1 = new SoundTemplate(N1_6_1, sources);

        firstDoorScript = firstDoor.GetComponent<DoorScript>();
        railTriggerClosingDoorScript = railTriggerClosingDoor.GetComponent<RailScriptV2>();
        railDouilleScript = railDouille.GetComponent<RailScriptV2>();
        railObstacleScript = railObstacle.GetComponent<RailScriptV2>();
        intersectionTutoDemiTourScript = intersectionTutoDemitour.GetComponent<RailScriptV2>();
        intersectionSansAigScript = intersectionSansAig.GetComponent<RailScriptV2>();
        railEvent01Script = railEvent01.GetComponent<RailScriptV2>();
        railDoorClosedScript = railDoorClosed.GetComponent<RailScriptV2>();
        iaVoiceScript = iaVoice.GetComponent<IAVoice1>();

        ugo = transform.Find("/Player");
        ugoMovement = ugo.GetComponent<RailMovementV2>();

        uTurnDone = false;

    }

	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(ugo.position, railTriggerClosingDoor.position) < 1f && ugo.position.z > railTriggerClosingDoor.position.z  && !sound1_1_1.isPlayed())
        {
            if (!iaVoiceScript.isPlaying())
            {
                previousSpeakers.gameObject.SetActive(false);
                previousLevel.gameObject.SetActive(false);
                previousSoundAmb.gameObject.SetActive(false);
                nextSpeakers.gameObject.SetActive(true);
                firstDoorScript.closeDoor();
                nextRail.gameObject.SetActive(true);
                previousRail.gameObject.SetActive(false);
                playSound(sound1_1_1);


                railTriggerClosingDoorScript.southRail = null;
                railTriggerClosingDoorScript.oneWay = false;
                railTriggerClosingDoorScript.isBlocked = true;
                railTriggerClosingDoorScript.connectRails();
            }
        }
        if (sound1_1_1.isPlayed() && sound1_1_1.isFinished() && !sound1_2_1.isPlayed())
        {
            playSound(sound1_2_1);
            
        }
        if (Vector3.Distance(ugo.position, railDouille.position) < 1f  && !sound1_2_2.isPlayed())
        {
            playSound(sound1_2_2);
            
        }
        if (ugoMovement.getIntersection() == railObstacleScript && !sound1_3_1.isPlayed())
        {
            playSound(sound1_3_1);
            
        }
        if (!sound1_3_2.isPlayed() && sound1_3_1.isFinished()) // && sfxAigActivation.isFinished)
        {
            playSound(sound1_3_2);
            
            intersectionTutoDemiTourScript.oneWay = false;
            StartCoroutine(testUturn());

        }
        if (sound1_3_2.isPlayed() && !sound1_3_4.isPlayed() && Camera.main.transform.eulerAngles.y > 160f && Camera.main.transform.eulerAngles.y < 200f)
        {
            playSound(sound1_3_4);
            
            uTurnDone = true;
        }
        if (sound1_3_4.isPlayed() && !sound1_4_1.isPlayed() && ugoMovement.getIntersection() == intersectionTutoDemiTourScript)
        {
            playSound(sound1_4_1);
            
        }
        if (sound1_4_1.isPlayed() && ugoMovement.getIntersection().aigPosition == RailScriptV2.AigPosition.aig1 && ugoMovement.getIntersection() == intersectionTutoDemiTourScript 
            && ugoMovement.isMovingForward && !sound1_4_2.isPlayed())
        {
            playSound(sound1_4_2);
            
        }
        if (ugoMovement.getIntersection() == intersectionSansAigScript && !sound1_4_3.isPlayed())
        {
            playSound(sound1_4_3);
            
        }
        if (ugoMovement.getIntersection() == railEvent01Script && !sound1_5.isPlayed())
        {
            playSound(sound1_5);
            
        }
        if (ugoMovement.getIntersection() == railDoorClosedScript && !sound1_6_1.isPlayed())
        {
            playSound(sound1_6_1);
            
        }
        		
	}

    private IEnumerator playSoundDelayed(SoundTemplate sound, float sec)
    {
        yield return new WaitForSeconds(sec);

        playSound(sound);
    }

    private void playSound(SoundTemplate sound)
    {
        sound.play();
        StartCoroutine(sound.endOfClip(0f));
    }

    private IEnumerator testUturn()
    {
        yield return new WaitForSeconds(delayBeforeUTurnReminder);
        if (!uTurnDone)
        {
            playSound(sound1_3_3);
            
        }
    }
}
