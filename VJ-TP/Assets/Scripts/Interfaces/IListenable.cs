using UnityEngine;

public interface IListenable
{
    AudioClip AudioClip { get; }
    AudioSource AudioSource { get; }

    void InitAudioSource();
    void PlayOnShot(AudioClip clip);
    void Play();
    void Stop();
}
