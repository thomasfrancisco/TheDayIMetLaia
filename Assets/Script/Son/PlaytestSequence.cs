using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaytestSequence : MonoBehaviour {
   

    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;
    public AudioClip clip7;
    public AudioClip clip8;

    public Transform audiolog;
    public Transform railAudiolog;
    public Transform railSwitch;

    private Transform[] puzzles;
    private Puzzle2Script [] puzScripts;
    private int currentLevel;
    private AudioSource source;
    private AudioLogBehaviour audiologScript;
    private RailMovementV2 ugoMovement;
    private RailScriptV2 railAudiologScript;
    private RailScriptV2 railSwitchScript;

    private SoundTemplate sound1;
    private SoundTemplate sound2;
    private SoundTemplate sound3;
    private SoundTemplate sound4;
    private SoundTemplate sound5;
    private SoundTemplate sound6;
    private SoundTemplate sound7;
    private SoundTemplate sound8;


	// Use this for initialization
	void Awake () {
        source = GetComponent<AudioSource>();
        audiologScript = audiolog.GetComponent<AudioLogBehaviour>();
        puzzles = new Transform[transform.childCount+1];
        puzScripts = new Puzzle2Script[transform.childCount+1];
        ugoMovement = transform.Find("/Player").GetComponent<RailMovementV2>();
        railAudiologScript = railAudiolog.GetComponent<RailScriptV2>();
        railSwitchScript = railSwitch.GetComponent<RailScriptV2>();
        currentLevel = 0;
		for(int i = 0; i < transform.childCount; i++)
        {
            puzzles[i] = transform.GetChild(i);
            puzScripts[i] = puzzles[i].GetChild(0).GetComponent<Puzzle2Script>();
        }
        sound1 = new SoundTemplate(clip1, source);
        sound2 = new SoundTemplate(clip2, source);
        sound3 = new SoundTemplate(clip3, source);
        sound4 = new SoundTemplate(clip4, source);
        sound5 = new SoundTemplate(clip5, source);
        sound6 = new SoundTemplate(clip6, source);
        sound7 = new SoundTemplate(clip7, source);
        sound8 = new SoundTemplate(clip8, source);
    }
	
	// Update is called once per frame
	void Update () {
        if (currentLevel < transform.childCount)
        {
            if (puzScripts[currentLevel].unlocked)
            {
                nextLevel();
            }
        }

        if (!sound1.isPlayed())
        {
            playSound(sound1);
        } else if (ugoMovement.getIntersection() == railAudiologScript
            && !sound2.isPlayed() && sound1.isPlayed() && !sound3.isPlayed())
        {
            playSound(sound2);
        } else if (audiologScript.isFinished && !sound3.isPlayed()) { 
            playSound(sound3);
        } else if (ugoMovement.getIntersection() == railSwitchScript
            && audiologScript.isFinished && !sound4.isPlayed())
        {
            playSound(sound4);
        } else if (currentLevel < transform.childCount && puzScripts[currentLevel].getNbMissed() == 1 && !sound5.isPlayed() && sound1.isPlayed() && sound2.isPlayed() && sound3.isPlayed() && sound4.isPlayed())
        {
            playSound(sound5);
        } else if (currentLevel < transform.childCount && puzScripts[currentLevel].getNbMissed() == 3
            && !sound6.isPlayed() && sound1.isPlayed() && sound2.isPlayed() && sound3.isPlayed() && sound4.isPlayed() && sound5.isPlayed())
        {
            playSound(sound6);
        } else if(currentLevel == 1 && !sound7.isPlayed())
        {
            playSound(sound7);
        } else if(currentLevel == transform.childCount && !sound8.isPlayed())
        {
            playSound(sound8);
        }
           
	}

    void nextLevel()
    {
        puzzles[currentLevel].gameObject.SetActive(false);
        currentLevel++;
        if(currentLevel < transform.childCount)
            puzzles[currentLevel].gameObject.SetActive(true);
    }

    private void playSound(SoundTemplate sound, float minusTime = 0)
    {
        sound.play();
        StartCoroutine(sound.endOfClip(minusTime));
    }

}
