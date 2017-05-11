using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonAiguillage : MonoBehaviour {

    public AudioClip activationSound;
    public AudioClip intersectionSound;

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
            source.clip = intersectionSound;
            source.Play();
        }
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
