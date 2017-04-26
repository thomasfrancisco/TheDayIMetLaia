using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonAiguillage : MonoBehaviour {

    public AudioClip aigActivation;

    private AudioSource source;


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

    public void playActivation()
    {
        source.clip = aigActivation;
        source.Play();
    }
}
