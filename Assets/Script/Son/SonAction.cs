using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonAction : MonoBehaviour {

    public AudioClip actionUseless;
    public AudioClip activation;

    private AudioSource source;


    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        source.clip = activation;
        source.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
