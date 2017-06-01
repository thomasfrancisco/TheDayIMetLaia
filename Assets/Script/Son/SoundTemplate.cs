using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTemplate {
    private AudioClip _clip; //Clip a jouer
    private bool _isPlayed;
    private List<AudioSource> _sources; //S'il y en a plusieurs
    public delegate void AudioCallback();
    private bool _finished;

    public SoundTemplate(AudioClip clip, AudioSource source)
    {
        _clip = clip;
        _sources = new List<AudioSource>();
        _sources.Add(source);
        _isPlayed = false;
        _finished = false;
    }

    public SoundTemplate(AudioClip clip, List<AudioSource> sources)
    {
        _clip = clip;
        _sources = sources;
        _isPlayed = false;
        _finished = false;
    }

    public void play()
    {
        _finished = false;
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
    
    public bool isFinished()
    {
        return _finished;
    }

    public void addSource(AudioSource source)
    {
        _sources.Add(source);
    }

    public IEnumerator endOfClip(float minusTime)
    {
        yield return new WaitForSeconds(_clip.length - minusTime);
        _finished = true;
        
    }

    public IEnumerator DelayedCallback(float time, AudioCallback callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
    


}
