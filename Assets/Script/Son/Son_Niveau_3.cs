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
    public Transform triggerRadioOn;
    public Transform railDeadEnd;
    public Transform railFrontSas;
    public Transform railInSas;
    public Transform doorSas;
    public Transform iaVoice4;
    public Transform doorExit;

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
    private DoorScript doorSasScript;
    private IAVoice4 iaVoice;
    private DoorScript doorExitScript;

    

    private void Awake()
    {
        ugo = transform.Find("/Player");
        ugoMovement = ugo.GetComponent<RailMovementV2>();

    }


    // Update is called once per frame
    void Update () {
		if(Vector3.Distance(ugo.position, triggerRadioOff.position) < 1f && !sound3_1.isPlayed())
        {
            playSound(sound3_1);
            foreach(Transform thing in previousThing)
            {
                thing.gameObject.SetActive(false);
            }
            foreach(Transform thing in nextThing)
            {
                thing.gameObject.SetActive(true);
            }
        }
        if(Vector3.Distance(ugo.position, triggerRadioOn.position) < 1f && !sound32_1.isPlayed())
        {
            playSound(sound32_1);
            StartCoroutine(playDelayed(sound32_2, N32_1.length + 3f));
            StartCoroutine(playDelayed(sound32_3, N32_1.length + N32_2.length + 3f));
        }

        if(Vector3.Distance(ugo.position, railDeadEnd.position) < 1f && !sound32_4.isPlayed())
        {
            playSound(sound32_4);
        }

        if(Vector3.Distance(ugo.position, railFrontSas.position) < 1f && !sound32_5.isPlayed())
        {
            playSound(sound32_5);
            doorSasScript.openDoor();
        }

        if(Vector3.Distance(ugo.position, railInSas.position) < 1f && !sound32_6_1.isPlayed())
        {
            playSound(sound32_6_1);
            StartCoroutine(playSasDial());
        }
        
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
    

    private IEnumerator playSasDial()
    {
        yield return new WaitForSeconds(N32_6_1.length);
        doorSasScript.closeDoor();
        yield return new WaitForSeconds(1f);
        playSound(sound32_6_1_2);
        yield return new WaitForSeconds(N32_6_1_2.length);
        iaVoice.playN32_2();
        yield return new WaitForSeconds(iaVoice.n32_2.length + 5f);
        iaVoice.playN32_3();
        yield return new WaitForSeconds(iaVoice.n32_3.length);
        playSound(sound32_6_2);
        yield return new WaitForSeconds(N32_6_2.length);
        iaVoice.playN32_4();
        yield return new WaitForSeconds(iaVoice.n32_4.length);
        playSound(sound32_6_3);
        yield return new WaitForSeconds(N32_6_3.length);
        //Debloquer rail
        yield return new WaitForSeconds(10f);
        if (!doorExitScript.isDoorOpen)
        {
            playSound(sound32_7);
        }



    }
    
}
