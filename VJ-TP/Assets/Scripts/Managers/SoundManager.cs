using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource Source;
    public AudioClip ClipVoice;
    public AudioClip MusicChill;
    public AudioClip hypedMusic;
    public AudioClip ScapeVoice;
    public AudioClip scapeMusic;

    private bool stopper = false;
    private bool waiterMeter = true;
    
    void Start()
    {
        EventsManager.instance.SoundStartEvent += PlayVoice;
        EventsManager.instance.StartBossMusicEvent += PlayBossMusicClip;
        EventsManager.instance.ScapeVoicelineEvent += PlayScapeVoice;
    }
    
    IEnumerator WaitForSound(Action play)
    {
        while (Source.isPlaying)
        {
            yield return null;
        }

        play();
    }
    
    IEnumerator SoundStopper(AudioSource s)
    {
        while (s.isPlaying && !stopper)
        {
            yield return null;
        }
        Source.Stop();
    }
    
    IEnumerator SleepAndFunc(float seconds, Action func)
    {
        yield return new WaitForSeconds(seconds);
        func();

    }

    public void PlayVoice()
    {
        Source.PlayOneShot(ClipVoice);
        Action act = PlayChillMusic;
        StartCoroutine(WaitForSound(act));
    }
    public void PlayChillMusic()
    {
        Source.clip = MusicChill;
        Source.Play();
        StartCoroutine(SoundStopper(Source));
    }
    
    public void PlayBossMusicClip()
    {
        if (Source.isPlaying)
        {
            stopper = true;
        }
        Source.clip = hypedMusic;

        Action a = () => Source.Play();
        StartCoroutine(SleepAndFunc(3, a));
        
        stopper = false;
        StartCoroutine(SoundStopper(Source));
    }
    public void PlayScapeVoice()
    {
        if (Source.isPlaying)
        {
            stopper = true;
            Source.Stop();
        }
        Source.PlayOneShot(ScapeVoice);
        
        
        Action a = PlayScapeMusic;

        Debug.Log("corrutine Started");
        
        StartCoroutine(WaitForSound(a));
    }
    
    public void PlayScapeMusic()
    {
        Debug.Log("SCAPEEEE MUSICCCC");
        Source.PlayOneShot(scapeMusic);
    }
    
}
