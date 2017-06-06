using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : MonoBehaviour {

    public AudioClip openingClip;
    public AudioClip closingClip;
    public AudioClip playerNearUnlockClip;
    public AudioClip playerNearLockClip;

    private bool isOpen;
    public bool isLocked;

    public float areaActivation;

    private Transform ugo;
    private AudioSource source;

    private SoundTemplate soundNearUnlock;
    private SoundTemplate soundNearLock;
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
        soundNearUnlock = new SoundTemplate(playerNearUnlockClip, source);
        soundNearLock = new SoundTemplate(playerNearLockClip, source);
        soundOpening = new SoundTemplate(openingClip, source);
        soundClosing = new SoundTemplate(closingClip, source);
        mesh = transform.GetChild(0);
        isOpen = false;
    }

    private void Update()
    {
        if(Vector3.Distance(ugo.position, transform.position) < areaActivation)
        {
            if (isLocked)
            {
                if (!soundNearLock.isPlayed())
                {
                    soundNearLock.play();
                }
            } else
            {

                if (!soundNearLock.isPlayed())
                {
                    isOpen = true;
                    mesh.gameObject.SetActive(false);
                    StartCoroutine(openDoorSound());
                }
            }
        } else
        {
            if (soundNearLock.isPlayed())
            {
                soundNearLock.setIsPlayed(false);
            }

            if (soundNearUnlock.isPlayed())
            {
                soundNearUnlock.setIsPlayed(false);
            }

            if (isOpen)
            {
                isOpen = false;
                mesh.gameObject.SetActive(true);
                soundClosing.play();
            }
        }
    }

    private IEnumerator openDoorSound()
    {
        soundNearUnlock.play();
        yield return new WaitForSeconds(playerNearUnlockClip.length);
        soundOpening.play();
    }


}
