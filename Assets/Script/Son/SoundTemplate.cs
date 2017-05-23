using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTemplate : MonoBehaviour{
    private AudioClip _clip; //Clip a jouer
    private bool _isPlayed;
    private List<AudioSource> _sources; //S'il y en a plusieurs
    public delegate void AudioCallback();

    public SoundTemplate(AudioClip clip, AudioSource source)
    {
        _clip = clip;
        _sources = new List<AudioSource>();
        _sources.Add(source);
        _isPlayed = false;
    }

    public SoundTemplate(AudioClip clip, List<AudioSource> sources)
    {
        _clip = clip;
        _sources = sources;
    }

    public void play()
    {
        foreach (AudioSource source in _sources)
        {
            source.clip = _clip;
            source.Play();
        }
        _isPlayed = true;
    }

    public bool isPlayed()
    {
        return _isPlayed;
    }

    public void setIsPlayed(bool isPlayed)
    {
        _isPlayed = isPlayed;
    }
    
    public bool hasFinished()
    {
        if(_sources[0].clip == _clip)
        {
            if(_sources[0].timeSamples == 0
                || _sources[0].timeSamples >= _sources[0].clip.samples- 5)
            {
                return true;
            }
        }
        return false;
    }

    public void addSource(AudioSource source)
    {
        _sources.Add(source);
    }

    public void playSoundWithCallback(AudioClip clip, AudioCallback callback)
    {
        foreach(AudioSource source in _sources)
        {
            source.PlayOneShot(clip);
        }
        StartCoroutine(DelayedCallback(clip.length, callback));
        
    }

    private IEnumerator DelayedCallback(float time, AudioCallback callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
    


}
