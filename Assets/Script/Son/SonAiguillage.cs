using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonAiguillage : MonoBehaviour {

    public AudioClip activationSound;
    public AudioClip accrocheSound;
    public AudioClip decrocheSound;
    public AudioClip validationSound;
    public AudioClip mouvementSound;

    
    private AudioSource source;

    private bool hasPlayed;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = false;
        hasPlayed = false;
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
        if(source.clip == validationSound)
        {
            if(source.timeSamples == source.clip.samples)
            {
                source.clip = mouvementSound;
                source.Play();
            }
        } else if (source.clip == mouvementSound)
        {
            if(source.timeSamples == source.clip.samples)
            {
                source.clip = null;
                return true;
            }
        } else
        {
            source.clip = validationSound;
            source.Play();
        }
        return false;
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
