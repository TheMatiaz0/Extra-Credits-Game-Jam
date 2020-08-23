using Cyberultimate;
using Cyberultimate.Unity;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioClip[] music;

    [SerializeField] private SerializedDictionary<string, AudioClip> soundEffects;

    
    [SerializeField, Range(0, 1)] private float musicVolume = 1;
    [SerializeField, Range(0, 1)] private float sfxVolume = 1;
    
    private int currentTrack = 0;
    private AudioSource sfxSource;
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        sfxSource = GetComponent<AudioSource>();
        sfxSource.volume = sfxVolume;
    }

    private void Start()
    {
        MusicControl.ChangeMusic(music[0]);
        MusicControl.Source.loop = true;
        MusicControl.PreferredAudioVolume = new Percent(musicVolume);
    }

    public void NextTrack()
    {
        if (currentTrack + 1 >= music.Length) return;
        
        currentTrack++;
        MusicControl.ChangeMusic(music[currentTrack]);
    }

    public void Stop()
    {
        MusicControl.Source.Stop();
    }

    public void PlaySFX(string name)
    {
        if (soundEffects.ContainsKey(name))
        {
            sfxSource.PlayOneShot(soundEffects[name]);
        }
    }
}
