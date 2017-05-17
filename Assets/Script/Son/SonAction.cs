using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonAction : MonoBehaviour {

    public AudioClip actionUseless;
    public AudioClip activation;

    private AudioSource source;
    private RailMovementV2 ugoMovementScript;


    private void Awake()
    {
        source = GetComponent<AudioSource>();
        ugoMovementScript = transform.parent.GetComponent<RailMovementV2>();
    }

    // Use this for initialization
    void Start () {
        source.clip = activation;
        //source.Play();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (ugoMovementScript.doAction)
        {
            source.clip = actionUseless;
            source.Play();
            ugoMovementScript.doAction = false;
        }

	}
    
}
