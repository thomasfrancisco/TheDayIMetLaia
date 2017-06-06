using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAVoice4 : MonoBehaviour {

    public AudioClip n32_1;
    public AudioClip n32_2;
    public AudioClip n32_3;
    public AudioClip n32_4;

    public Transform trigAirlock;
    public Transform speakerIAexitAirlock;
    public Transform speakerIAairlock;

    private SoundTemplate sound32_1;
    private SoundTemplate sound32_2;
    private SoundTemplate sound32_3;
    private SoundTemplate sound32_4;

    private TriggerIA triggerAirlock;
    private AudioSource sourceExitAirlock;
    private AudioSource sourceAirlock;

    private Transform ugo;
    
    private void Awake()
    {
        ugo = transform.Find("/Player");
        sourceAirlock = speakerIAairlock.GetComponent<AudioSource>();
        sourceExitAirlock = speakerIAexitAirlock.GetComponent<AudioSource>();
        sound32_1 = new SoundTemplate(n32_1, sourceExitAirlock);
        sound32_2 = new SoundTemplate(n32_2, sourceAirlock);
        sound32_3 = new SoundTemplate(n32_3, sourceAirlock);
        sound32_4 = new SoundTemplate(n32_4, sourceAirlock);
        triggerAirlock = trigAirlock.GetComponent<TriggerIA>();
    }

    private void Update()
    {
        if(Vector3.Distance(ugo.position, trigAirlock.position) < triggerAirlock.distanceArea
            && !sound32_1.isPlayed())
        {
            playSound(sound32_1);
        }
    }

    public void playN32_2()
    {
        playSound(sound32_2);
    }

    public void playN32_3()
    {
        playSound(sound32_3);
    }

    public void playN32_4()
    {
        playSound(sound32_4);
    }

    private void playSound(SoundTemplate sound)
    {
        sound.play();
        StartCoroutine(sound.endOfClip(0f));
    }
}
