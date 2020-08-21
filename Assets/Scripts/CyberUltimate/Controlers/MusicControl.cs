
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
namespace Cyberultimate.Unity
{
    public static class MusicControl
    {
        public enum Mode
        {
            Normal,
            FastHidePreviousClip,
            Immediately,
        }
        private static AudioSource _source;
        private static AudioClip nextClip;
        private static bool hasNextClip;//next clip can be null.
        public static AudioSource Source
        {
            get
            {
                if (System.Object.ReferenceEquals(_source, null))//if object with DontDestroyOnLoad died, it means it's closing context
                {
                    GameObject g = new GameObject();
                    UnityEngine.Object.DontDestroyOnLoad(g);
                    g.name = "_Source";
                    //g.hideFlags |= HideFlags.HideInHierarchy;      
                    _source = g.AddComponent<AudioSource>();
                    _source.outputAudioMixerGroup = (Resources.Load("Audio") as AudioMixer).FindMatchingGroups("Music")[0];
                }
                return _source;
            }
        }
        public static bool HasClip => Source.clip != null;
        public static AudioClip CurrentClip => (hasNextClip) ? nextClip : Source.clip;
        private static Percent _PreferredAudioVolume = new Percent(1);
        public static Percent PreferredAudioVolume
        {
            get => _PreferredAudioVolume;
            set
            {
                _PreferredAudioVolume = value;
                RefreshTweens();
            }
        }
        private static TimeSpan _PreferredTimeToFullAudio = TimeSpan.FromSeconds(5f);

        public static TimeSpan PreferredTimeToFullAudio
        {
            get => _PreferredTimeToFullAudio;
            set
            {
                _PreferredTimeToFullAudio = value;
                RefreshTweens();
            }
        }
        private static TimeSpan? _CustomPreferredTimeForHided = null;
        public static TimeSpan? CustomPreferredTimeForHided
        {
            get => _CustomPreferredTimeForHided;
            set
            {
                _CustomPreferredTimeForHided = value;
                RefreshTweens();
            }
        }

        private static TimeSpan _PreferredTimeForAdditionalFades = TimeSpan.FromSeconds(10f);
        public static TimeSpan PreferredTimeForAdditionFades
        {
            get => _PreferredTimeForAdditionalFades;
            set
            {
                _PreferredTimeForAdditionalFades = value;
                RefreshTweens();
            }
        }
        private static Percent _PreferredPitch = new Percent(1);
        public static Percent PreferredPitch
        {
            get => _PreferredPitch;
            set
            {
                _PreferredPitch = value;
                RefreshTweens();
            }
        }
        private static void RefreshTweens()
        {
            LeanTween.cancel(Source.gameObject);


            float toGo;
            float multiply;
            TimeSpan baseTime;
            if (hasNextClip)
                baseTime = CustomPreferredTimeForHided ?? PreferredTimeToFullAudio;
            else
                baseTime = PreferredTimeToFullAudio;
            if (hasNextClip == false)
            {

                multiply = (float)(PreferredAudioVolume.AsFloat - Source.volume) / PreferredAudioVolume.AsFloat;

                if (multiply > 1)
                    multiply -= 1;

                toGo = PreferredAudioVolume.AsFloat;
            }
            else
            {
                multiply = (float)(1 - ((PreferredAudioVolume.AsFloat - Source.volume) / PreferredAudioVolume.AsFloat));
                toGo = 0;
            }

            LTDescr descr = LeanTween.value(Source.gameObject,
             (v) =>
             {
                 Source.volume = v;
             }, Source.volume, toGo, (float)(baseTime.TotalSeconds * multiply)).setIgnoreTimeScale(true);
            if (hasNextClip)
                descr.setOnComplete(() => { SetClip(nextClip); RefreshTweens(); });


            AddSettingsTween(PreferredPitch.AsFloat, Source.pitch, (v) => Source.pitch = v);

        }
        private static void AddSettingsTween(float preffered, float cur, Action<float> setter)
        {
            float fullTime = (float)Math.Abs((preffered - cur) * PreferredTimeForAdditionFades.TotalSeconds);
            LeanTween.value(Source.gameObject, setter, cur, preffered, fullTime).setIgnoreTimeScale(true);
        }


        public static void ChangeMusic(AudioClip clip, Mode mode = Mode.Normal)
        {
            if (Source.clip == null)
            {
                if (hasNextClip)
                {
                    nextClip = clip;
                    hasNextClip = true;
                }
                else
                {
                    Source.volume = 0;

                    SetClip(clip);
                }

            }
            else
            {
                nextClip = clip;
                hasNextClip = true;
            }
            switch (mode)
            {
                case Mode.Immediately:
                    SetClip(clip);
                    Source.volume = PreferredAudioVolume.AsFloat;
                    break;
                case Mode.FastHidePreviousClip:
                    SetClip(clip);
                    Source.volume = 0;
                    break;
            }


            RefreshTweens();
        }
        private static void SetClip(AudioClip clip)
        {
            Source.clip = clip;
            Source.Play();
            hasNextClip = false;
        }

    }
}
