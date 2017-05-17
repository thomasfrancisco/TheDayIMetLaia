using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonChoc : MonoBehaviour {

    public AudioClip collisionSound;

    private AudioSource source;
    public float frequencyHit;
    private float timer;
    private bool hasPlayed;


    private void Awake()
    {
        timer = 0f;
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

    public void playCollision()
    {
        if (!source.isPlaying && !hasPlayed)
        {
            source.clip = collisionSound;
            source.Play();
            hasPlayed = true;
        }
    }

    public void resetCollision()
    {
        hasPlayed = false;
    }
}
