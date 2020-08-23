using Cyberultimate.Unity;
using UnityEngine;

public class MusicManager : MonoSingleton<MusicManager>
{
    [SerializeField] private AudioClip[] music;
    private int currentTrack = 0;
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        MusicControl.ChangeMusic(music[0]);
        MusicControl.Source.loop = true;
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
}
