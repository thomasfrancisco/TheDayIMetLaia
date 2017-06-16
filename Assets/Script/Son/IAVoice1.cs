using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAVoice1 : MonoBehaviour {
    public AudioClip ia_n1_1;
    public Transform trigCrewQuarter;

    private AudioSource source;

    private SoundTemplate sound1_1;
    private Transform ugo;

    private TriggerIA triggerCrewQuarter;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        sound1_1 = new SoundTemplate(ia_n1_1, source);
        triggerCrewQuarter = trigCrewQuarter.GetComponent<TriggerIA>();
        ugo = transform.Find("/Player");
    }

	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(ugo.position, trigCrewQuarter.position) < triggerCrewQuarter.distanceArea && !sound1_1.isPlayed())
        {
            playSound(sound1_1);
        }
	}

    private void playSound(SoundTemplate sound)
    {
        sound.play();
        StartCoroutine(sound.endOfClip(0f));
    }

    public bool isPlaying()
    {
        return source.isPlaying;
    }
}
