using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSound : MonoBehaviour {

    public AudioClip[] sons;

    private AudioSource source;
    private int currentSound;

    private void Awake()
    {
        currentSound = 0;
        source = GetComponent<AudioSource>();
        source.clip = sons[currentSound];
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
        currentSound = (currentSound + 1) % sons.Length;
        source.clip = sons[currentSound];
    }
}
