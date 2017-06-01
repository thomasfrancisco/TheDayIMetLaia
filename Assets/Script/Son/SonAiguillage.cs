using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonAiguillage : MonoBehaviour {

    public AudioClip activationSound;
    public AudioClip accrocheSound;
    public AudioClip decrocheSound;
    public AudioClip validation;
    public AudioClip mouvement;

    private SoundTemplate validationSound;
    private SoundTemplate mouvementSound;

    private bool isCurrentlyMoving;
    
    private AudioSource source;

    private bool hasPlayed;

    private void Awake()
    {
        isCurrentlyMoving = false;
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = false;
        hasPlayed = false;
        validationSound = new SoundTemplate(validation, source);
        mouvementSound = new SoundTemplate(mouvement, source);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playIntersection()
    {
        if (!hasPlayed)
        {
            hasPlayed = true;
            source.clip = accrocheSound;
            source.Play();
        }
    }

    public bool playMouvement()
    {
        if (!validationSound.isPlayed())
        {
            isCurrentlyMoving = true;
            StartCoroutine(routineValidation());
        }
        return !isCurrentlyMoving;
    }

    private IEnumerator routineValidation()
    {
        validationSound.play();
        yield return new WaitForSeconds(validation.length);
        StartCoroutine(routineMouvement());
    }

    private IEnumerator routineMouvement()
    {
        mouvementSound.play();
        yield return new WaitForSeconds(mouvement.length);
        isCurrentlyMoving = false;
        yield return new WaitForSeconds(.5f);
        validationSound.setIsPlayed(false);
        mouvementSound.setIsPlayed(false);
    }

    public void playDecroche()
    {
        source.clip = decrocheSound;
        source.Play();
    }


    public void reset()
    {
        hasPlayed = false;
    }

    public void playActivation()
    {
      source.clip = activationSound;
      source.Play();
    }
}
