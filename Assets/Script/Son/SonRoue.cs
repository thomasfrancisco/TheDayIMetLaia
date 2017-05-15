using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonRoue : MonoBehaviour {
    private AudioSource source;
    public AudioClip roueDemiTour;


    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playSoundDemiTour()
    {
        source.clip = roueDemiTour;
        source.Play();
    }
}
