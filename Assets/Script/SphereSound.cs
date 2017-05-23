using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSound : MonoBehaviour {



    public AudioClip[] sounds;

    private AudioSource source;
    [HideInInspector]
    public int currentSound;

    private void Awake()
    {
        currentSound = 0;
        source = GetComponent<AudioSource>();
        source.clip = sounds[currentSound];
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playSound()
    {
        source.Play();

    }

    public void increaseSoundValue()
    {
        currentSound = (currentSound + 1) % sounds.Length;
        source.clip = sounds[currentSound];
    }
}
