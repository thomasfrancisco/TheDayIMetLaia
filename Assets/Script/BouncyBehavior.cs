using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBehavior : MonoBehaviour {

    public float lifetime;
    public AudioClip bounceSound;

    
    private AudioSource audioBounceSound;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        audioBounceSound = gameObject.AddComponent<AudioSource>();
        audioBounceSound.clip = bounceSound;
        audioBounceSound.playOnAwake = false;
        audioBounceSound.loop = false;
        Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision coll)
    {
        
        audioBounceSound.volume = rb.velocity.magnitude;
        audioBounceSound.Play();
    }
}
