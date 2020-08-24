using System;
using System.Threading.Tasks;
using Cyberultimate.Unity;
using UnityEngine;


public class HomeMusic : MonoSingleton<HomeMusic>
{
    [Range(0, 1)] public float musicVolume = 1;

    [SerializeField] private AudioClip[] music;

    [SerializeField] private AudioSource source;


    private int currentTrack = 0;

    private void Start()
    {
        source.clip = music[currentTrack];
        source.loop = true;
        source.volume = musicVolume;
        source.Play();
    }

    public void NextTrack()
    {
        if (currentTrack + 1 >= music.Length) return;
        
        FadeVolume(source.volume, 0, () =>
        {
            currentTrack++;
            source.clip = music[currentTrack];
            FadeVolume(0, musicVolume);
        });
        
    }

    private void FadeVolume(float from, float to, Action callback = null)
    {
        LeanTween.value(source.gameObject,
                (v) =>
                {
                    source.volume = v;
                }, source.volume, 0, (float) (0.5f)).setIgnoreTimeScale(true)
            .setOnComplete(_ =>
            {
                callback?.Invoke();
            });
    }
}
