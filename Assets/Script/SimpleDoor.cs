using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : MonoBehaviour {

    public AudioClip openingClip;
    public AudioClip closingClip;

    private bool isOpen;
    public bool isLocked;

    public float areaActivation;

    private Transform ugo;
    private AudioSource source;
    
    private SoundTemplate soundOpening;
    private SoundTemplate soundClosing;

    private Transform mesh;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, areaActivation);
    }

    private void Awake()
    {
        ugo = transform.Find("/Player");
        source = GetComponent<AudioSource>();
        soundOpening = new SoundTemplate(openingClip, source);
        soundClosing = new SoundTemplate(closingClip, source);
        mesh = transform.GetChild(0);
        isOpen = false;
    }

    private void Update()
    {
        if (Vector3.Distance(ugo.position, transform.position) < areaActivation)
        {
            if (!soundOpening.isPlayed())
            {
                playSound(soundOpening);
                soundClosing.setIsPlayed(false);
            }
        } else
        {
            if (!soundClosing.isPlayed())
            {
                playSound(soundClosing);
                soundOpening.setIsPlayed(false);
            }
        }
    }


    public void closeDoor()
    {
        isOpen = false;
        mesh.gameObject.SetActive(true);
        soundClosing.play();
    }

    private void playSound(SoundTemplate sound)
    {
        sound.play();
        StartCoroutine(sound.endOfClip(0f));
    }

}
