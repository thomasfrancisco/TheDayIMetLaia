using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxSound : MonoBehaviour
{

    public float radius;
    public AudioClip clip;
    public float coeffFade;
    public bool oneShot;
    public bool notAuto;
    

    private AudioSource source;
    private Transform ugo;
    private RailMovementV2 ugoMovement;
    private float volumeMax;
    private bool isPlayed;

    private void Awake()
    {
        ugo = transform.Find("/Player");
        ugoMovement = ugo.GetComponent<RailMovementV2>();
        source = GetComponent<AudioSource>();
        source.playOnAwake = !oneShot;
        source.loop = !oneShot;
        volumeMax = source.volume;
        source.volume = 0;
        source.clip = clip;
        if(!oneShot)
            source.Play();
        isPlayed = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Update()
    {
        if (!oneShot)
        {
            if (Vector3.Distance(transform.position, ugo.position) < radius)
            {
                if (ugoMovement.isMovingForward || ugoMovement.isMovingBackward)
                {
                    fadeIn();
                }
                else
                {

                    fadeOut();
                }
            }
            else
            {
                fadeOut();
            }
        } else
        {
            if (Vector3.Distance(transform.position, ugo.position) < radius)
            {
                if (!notAuto)
                {
                    if (!isPlayed)
                    {
                        source.volume = volumeMax;
                        source.Play();
                        isPlayed = true;
                    }
                }
            }
        }


    }

    public void play(float delay  = 0f)
    {
        if(!isPlayed)
            StartCoroutine(playDelayed(delay));
    }

    public void stop()
    {
        StartCoroutine(fadingStop());
    }

    private void fadeIn()
    {
        if (source.volume < volumeMax)
        {
            source.volume += Time.deltaTime * coeffFade;
        }
    }

    private void fadeOut()
    {
        if (source.volume > 0)
        {
            source.volume -= Time.deltaTime * coeffFade;
        }
    }

    private IEnumerator fadingStop()
    {
        for(int i = 0; i < 100; i++)
        {
            source.volume -= source.volume * 1 / 100;
            yield return new WaitForSeconds(0.1f);
        }
        source.Stop();
    }

    private IEnumerator playDelayed(float time)
    {
        yield return new WaitForSeconds(time);
        source.volume = volumeMax;
        source.Play();
        isPlayed = true;
    }

    public AudioSource getSource()
    {
        return source;
    }
}
