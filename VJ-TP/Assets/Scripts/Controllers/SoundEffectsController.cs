using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectsController : MonoBehaviour, IListenable
{
    // Assignated audio in inspector
    public AudioClip AudioClip => _audioClip;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioClip _victoryClip;
    [SerializeField] private AudioClip _defeatClip;

    public AudioSource AudioSource => _audioSource;
    private AudioSource _audioSource;

    public void InitAudioSource()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = AudioClip;
    }

    public void PlayOnShot(AudioClip clip) => AudioSource.PlayOneShot(clip);

    public void Play() => AudioSource.Play();

    public void Stop() => AudioSource.Stop();

    void Start() {
        InitAudioSource();
        EventsManager.instance.OnGameOver += OnGameOver;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.O)) PlayOnShot(_audioClip);
        if (Input.GetKeyDown(KeyCode.P)) Play();
    }

    private void OnGameOver(bool isVictory) {
        if (isVictory)  PlayOnShot(_victoryClip);
        else            PlayOnShot(_defeatClip);
    }
}
