using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSound : MonoBehaviour {



    public AudioClip[] sounds;
    public AudioClip hover;

    private AudioSource source;
    private AudioSource childSource;
    [HideInInspector]
    public int currentSound;

    private void Awake()
    {
        currentSound = 0;
        source = GetComponent<AudioSource>();
        source.clip = sounds[currentSound];
        childSource = transform.FindChild("hoverSound").GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playHover()
    {
        if (!childSource.isPlaying)
        {
            childSource.clip = hover;
            childSource.Play();
        }
    }

    public void stopHover()
    {
        if(childSource.isPlaying)
        {
            childSource.Stop();
        }
    }

    public void playSound()
    {
        source.clip = sounds[currentSound];
        source.Play();       
    }

    public void increaseSoundValue()
    {
        currentSound = (currentSound + 1) % sounds.Length;
        source.clip = sounds[currentSound];
    }
}
