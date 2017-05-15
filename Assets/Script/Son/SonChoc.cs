using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonChoc : MonoBehaviour {

    public AudioClip collisionSound;

    private AudioSource source;
    public float frequencyHit;
    private float timer;


    private void Awake()
    {
        timer = 0f;
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

    public void playCollision()
    {
        if (!source.isPlaying)
        {
            
            if (timer == 0)
            {
                source.clip = collisionSound;
                source.Play();
            }
            timer += Time.deltaTime;
            if (timer > frequencyHit)
                timer = 0f;
            
        }
    }

    public void resetCollision()
    {
        timer = 0f;
    }
}
