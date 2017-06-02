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

    private SoundTemplate sound0_1_1;
    private SoundTemplate sound0_1_2;
    private SoundTemplate sound0_1_3;
    private SoundTemplate sound0_1_4;
    private SoundTemplate sound0_2_1;
    private SoundTemplate sound0_2_2;
    private SoundTemplate sound0_2_3;
    private SoundTemplate sound0_3_1;
    private SoundTemplate sound0_3_2;
    private SoundTemplate sound0_3_3;
    private SoundTemplate sound0_3_4;
    private SoundTemplate sound0_3_5;


    public Transform puzzle1;
    public Transform ugo;
    public Transform blockingRail;
    public float minAngleHead;
    public float timeBeforeRepeat;

    public Transform speakers1;
    public Transform speakers2;
    public Transform railNiv1;
    public Transform railNiv2;

    private List<AudioSource> sources;
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
        sources = new List<AudioSource>(GetComponentsInChildren<AudioSource>());
        sound0_1_1 = new SoundTemplate(ComVX_N0_1_1, sources);
        sound0_1_2 = new SoundTemplate(ComVX_N0_1_2, sources);
        sound0_1_3 = new SoundTemplate(ComVX_N0_1_3, sources);
        sound0_1_4 = new SoundTemplate(ComVX_N0_1_4, sources);
        sound0_2_1 = new SoundTemplate(ComVX_N0_2_1, sources);
        sound0_2_2 = new SoundTemplate(ComVX_N0_2_2, sources);
        sound0_2_3 = new SoundTemplate(ComVX_N0_2_3, sources);
        sound0_3_1 = new SoundTemplate(ComVX_N0_3_1, sources);
        sound0_3_2 = new SoundTemplate(ComVX_N0_3_2, sources);
        sound0_3_3 = new SoundTemplate(ComVX_N0_3_3, sources);
        sound0_3_4 = new SoundTemplate(ComVX_N0_3_4, sources);
        sound0_3_5 = new SoundTemplate(ComVX_N0_3_5, sources);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!sound0_1_1.isPlayed())
        {
            playSound(sound0_1_1);
            StartCoroutine(reminder(ComVX_N0_1_1, sound0_1_2, sound0_1_3));
            previousHeadRotationY = Camera.main.transform.rotation.y;
        }

        if(sound0_1_1.isPlayed() && Camera.main.transform.rotation.y < previousHeadRotationY - minAngleHead
            && !sound0_1_3.isPlayed() && sound0_1_1.isFinished())
        {
            playSound(sound0_1_3);
            StartCoroutine(reminder(ComVX_N0_1_3, sound0_1_4, sound0_2_1));
            previousHeadRotationY = Camera.main.transform.rotation.y;
        }
        
        if(sound0_1_3.isPlayed() && Camera.main.transform.rotation.y > previousHeadRotationY + minAngleHead
            && !sound0_2_1.isPlayed() && sound0_1_3.isFinished())
        {
            playSound(sound0_2_1);
            StartCoroutine(playRailActivation(ComVX_N0_2_1.length));
        }

        if(sound0_2_2.isPlayed() && ugoRailMovement.isMovingForward && !sound0_3_1.isPlayed())
        {
            playSound(sound0_3_1);
        }

        if(ugoRailMovement.getIntersection() == blockingRailScript && !sound0_3_2.isPlayed())
        {
            playSound(sound0_3_2);
        }

        if(doorScript.isHatchOpen && !sound0_3_3.isPlayed())
        {
            playSound(sound0_3_3);
        }

        if(doorScript.getNbMissed() == 3 && !sound0_3_4.isPlayed())
        {
            playSound(sound0_3_4);
        }

        if(doorScript.isDoorOpen && !sound0_3_5.isPlayed())
        {
            playSound(sound0_3_5);
        }
    }

    private void playSound(SoundTemplate sound)
    {
        sound.play();
        StartCoroutine(sound.endOfClip(0f));
    }

    private IEnumerator reminder(AudioClip clipToEnd, SoundTemplate soundToPlay, SoundTemplate soundToTest)
    {
        yield return new WaitForSeconds(timeBeforeRepeat + clipToEnd.length);
        if (!soundToTest.isPlayed())
        {
            playSound(soundToPlay);
        }
    }

    private IEnumerator playRailActivation(float time)
    {
        yield return new WaitForSeconds(time);
        sonAiguillage.playActivation();
        ugoRailMovement.avanceDebloque = true;
        ugoRailMovement.reculeDebloque = true;
        yield return new WaitForSeconds(sonAiguillage.activationSound.length);
        playSound(sound0_2_2);
        StartCoroutine(reminder(ComVX_N0_2_2, sound0_2_3, sound0_3_1));
    }

    
    
}
