﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maeul
{
    public class AudioManager : MonoBehaviour
    {
        #region SingleTon
        /* SingleTon */
        private static AudioManager instance;
        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType(typeof(AudioManager)) as AudioManager;
                    if (!instance)
                    {
                        Debug.LogError("AudioManager Not Exsist In This Scene");                        
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Values
        public Dictionary<string, AudioClip> DataDic;

        private AudioSource audioSource;
        private AudioSource soundEffectAudioSource;
        #endregion

        #region Volume Setting Properties

        private float bgmVolume;
        public float BgmVolume
        {
            get
            {
                return bgmVolume;
            }

            set
            {
                if (value <= 0)
                {
                    value = 0;
                }

                bgmVolume = value;

                audioSource.volume = bgmVolume;
            }
        }

        private float soundEffectVolume;
        public float SoundEffectVolume
        {
            get
            {
                return soundEffectVolume;
            }

            set
            {
                if (value <= 0)
                {
                    value = 0;
                }

                soundEffectVolume = value;
                ApplySoundEffectVolume();
            }
        }

        #endregion


        private void Awake()
        {
            #region SingleTone

            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this);
            }

            #endregion

            
        }

        public void Init()
        {
            /* Clip Dictionary Init */
            DataDic = new Dictionary<string, AudioClip>();

            AudioClip[] datas = Resources.LoadAll<AudioClip>("AudioClip/");
            for (int i = 0; i < datas.Length; i++)
            {
                DataDic.Add(datas[i].name, datas[i]);
            }

            /* Component Init */
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            // 201021_jjshin : soundEffectAudioSource 초기화
            soundEffectAudioSource = gameObject.AddComponent<AudioSource>();
        }

        public void StartBGM()
        {
            audioSource.Play();
        }

        public void PlayOneShot(string clipName)
        {
            if (!DataDic.ContainsKey(clipName))
            {
                Debug.LogError(string.Format("AudioManager-DataDic Has No key {0}", clipName));
                return;
            }

            soundEffectAudioSource.PlayOneShot(DataDic[clipName]);
        }

        public void SetBgmVolume(float volume)
        {
            BgmVolume = volume;
        }

        public void SetSoundEffectVolume(float volume)
        {
            SoundEffectVolume = volume;
        }

        public void ApplySoundEffectVolume()
        {
            soundEffectAudioSource.volume = soundEffectVolume;
        }
    }
}
