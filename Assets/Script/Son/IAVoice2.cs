using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAVoice2 : MonoBehaviour {

    public AudioClip ia_n2_1;
    public AudioClip ia_n2_2;


    public Transform trigGreenhouse;
    public Transform trigControlCenter;
    public List<Transform> speakerGreenhouse;
    public Transform speakerControlCenter;

    private List<AudioSource> sourcesGreenhouse;
    private AudioSource sourceControlCenter;


    private SoundTemplate sound2_1;
    private SoundTemplate sound2_2;

    private Transform ugo;
    private TriggerIA triggerGreenhouse;
    private TriggerIA triggerControlCenter;


    private void Awake()
    {
        ugo = transform.Find("/Player");
        triggerGreenhouse = trigGreenhouse.GetComponent<TriggerIA>();
        triggerControlCenter = trigControlCenter.GetComponent<TriggerIA>();
        sourcesGreenhouse = new List<AudioSource>();
        foreach (Transform t in speakerGreenhouse)
        {
            sourcesGreenhouse.Add(t.GetComponent<AudioSource>());
        }
        sourceControlCenter = speakerControlCenter.GetComponent<AudioSource>();


        sound2_1 = new SoundTemplate(ia_n2_1, sourcesGreenhouse);
        sound2_2 = new SoundTemplate(ia_n2_2, sourceControlCenter);
    }

    // Update is called once per frame
    void Update () {
		if(Vector3.Distance(ugo.position, trigGreenhouse.position) < triggerGreenhouse.distanceArea)
        {
            if (!sound2_1.isPlayed())
            {
                playSound(sound2_1);
            }
        } else
        {
            sound2_1.setIsPlayed(false);
        }

        if(Vector3.Distance(ugo.position, trigControlCenter.position) < triggerControlCenter.distanceArea)
        {
            if (!sound2_2.isPlayed())
            {
                playSound(sound2_2);
            }
        } else
        {
            sound2_2.setIsPlayed(false);
        }

	}

    private void playSound(SoundTemplate sound)
    {
        sound.play();
        StartCoroutine(sound.endOfClip(0f));
    }

    public bool isPlaying()
    {

        foreach(AudioSource source in sourcesGreenhouse)
        {
            if (source.isPlaying)
                return true;
        }

        return sourceControlCenter.isPlaying;
    }
}
