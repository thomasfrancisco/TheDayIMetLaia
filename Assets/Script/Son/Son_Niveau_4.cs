using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Son_Niveau_4 : MonoBehaviour {

    public List<Transform> previousThing;

    public AudioClip n4_1;
    public AudioClip n4_2;
    public AudioClip n4_3;
    public AudioClip n4_4;
    public AudioClip n4_5;
    public AudioClip n4_6;

    private SoundTemplate sound4_1;
    private SoundTemplate sound4_2;
    private SoundTemplate sound4_3;
    private SoundTemplate sound4_4;
    private SoundTemplate sound4_5;
    private SoundTemplate sound4_6;

    private AudioSource source;

	// Use this for initialization
	void Awake () {
        source = GetComponentInChildren<AudioSource>();
        sound4_1 = new SoundTemplate(n4_1, source);
        sound4_2 = new SoundTemplate(n4_2, source);
        sound4_3 = new SoundTemplate(n4_3, source);
        sound4_4 = new SoundTemplate(n4_4, source);
        sound4_5 = new SoundTemplate(n4_5, source);
        sound4_6 = new SoundTemplate(n4_6, source);
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!sound4_1.isPlayed())
        {
            foreach(Transform thing in previousThing)
            {
                thing.gameObject.SetActive(false);
            }
            StartCoroutine(playFinal());
            
        }
	}

    private IEnumerator playFinal()
    {
        //Faudra ajouter tous les sons d'ambiance
        playSound(sound4_1);
        yield return new WaitForSeconds(n4_1.length + 1f);
        playSound(sound4_2);
        yield return new WaitForSeconds(n4_2.length + 1f);
        playSound(sound4_3);
        yield return new WaitForSeconds(n4_3.length + 1f);
        playSound(sound4_4);
        yield return new WaitForSeconds(n4_4.length + 1f);
        playSound(sound4_5);
        yield return new WaitForSeconds(n4_5.length + 1f);
        playSound(sound4_6);
        yield return new WaitForSeconds(n4_6.length + 1f);
    }

    private void playSound(SoundTemplate sound)
    {
        sound.play();
        StartCoroutine(sound.endOfClip(0f));
    }
}
