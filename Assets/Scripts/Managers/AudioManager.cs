using System;
using System.Collections.Generic;
using Cyberultimate;
using Cyberultimate.Unity;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    
    [SerializeField] private SerializedDictionary<string, AudioClip> soundEffects;

    [Range(0, 1)] public float sfxVolume = 1;
    
    private AudioSource sfxSource;


    private Queue<AudioClip> dialogQueue = new Queue<AudioClip>();
    private bool dialogInProgress = false;

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);

        sfxSource = GetComponent<AudioSource>();
        sfxSource.volume = sfxVolume;
    }


    public void PlaySFX(string name)
    {
        if (soundEffects.ContainsKey(name))
        {
            sfxSource.PlayOneShot(soundEffects[name]);
        }
        else
        {
            throw new KeyNotFoundException("SFX with given key not found");
        }
    }

    public void PlayClip(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
