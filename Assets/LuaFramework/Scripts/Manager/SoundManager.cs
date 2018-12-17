using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace LuaFramework
{
    public class SoundManager : Manager
    {
        private AudioSource audio;
        private Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
        private Dictionary<string, AudioSource> dicAudios = new Dictionary<string, AudioSource>();
        private string backSoundKey = "";
        private delegate void GetBack(AudioClip clip, string key);

        #region ‘≠¬ﬂº≠

        //void Start()
        //{
        //    audio = GetComponent<AudioSource>();
        //}

        ///// <summary>
        ///// ÃÌº”“ª∏ˆ…˘“Ù
        ///// </summary>
        //void Add(string key, AudioClip value)
        //{
        //    if (sounds[key] != null || value == null) return;
        //    sounds.Add(key, value);
        //}

        ///// <summary>
        ///// ªÒ»°“ª∏ˆ…˘“Ù
        ///// </summary>
        //AudioClip Get(string key)
        //{
        //    if (sounds[key] == null) return null;
        //    return sounds[key] as AudioClip;
        //}

        ///// <summary>
        ///// ‘ÿ»Î“ª∏ˆ“Ù∆µ
        ///// </summary>
        //public AudioClip LoadAudioClip(string path)
        //{
        //    AudioClip ac = Get(path);
        //    if (ac == null)
        //    {
        //        ac = (AudioClip)Resources.Load(path, typeof(AudioClip));
        //        Add(path, ac);
        //    }
        //    return ac;
        //}



        ///// <summary>
        /////  «∑Ò≤•∑≈±≥æ∞“Ù¿÷£¨ƒ¨»œ «1£∫≤•∑≈
        ///// </summary>
        ///// <returns></returns>
        //public bool CanPlayBackSound()
        //{
        //    string key = AppConst.AppPrefix + "BackSound";
        //    int i = PlayerPrefs.GetInt(key, 1);
        //    return i == 1;
        //}

        ///// <summary>
        ///// ≤•∑≈±≥æ∞“Ù¿÷
        ///// </summary>
        ///// <param name="canPlay"></param>
        //public void PlayBacksound(string name, bool canPlay)
        //{
        //    if (audio.clip != null)
        //    {
        //        if (name.IndexOf(audio.clip.name) > -1)
        //        {
        //            if (!canPlay)
        //            {
        //                audio.Stop();
        //                audio.clip = null;
        //                Util.ClearMemory();
        //            }
        //            return;
        //        }
        //    }
        //    if (canPlay)
        //    {
        //        audio.loop = true;
        //        audio.clip = LoadAudioClip(name);
        //        audio.Play();
        //    }
        //    else
        //    {
        //        audio.Stop();
        //        audio.clip = null;
        //        Util.ClearMemory();
        //    }
        //}

        ///// <summary>
        /////  «∑Ò≤•∑≈“Ù–ß,ƒ¨»œ «1£∫≤•∑≈
        ///// </summary>
        ///// <returns></returns>
        //public bool CanPlaySoundEffect()
        //{
        //    string key = AppConst.AppPrefix + "SoundEffect";
        //    int i = PlayerPrefs.GetInt(key, 1);
        //    return i == 1;
        //}

        ///// <summary>
        ///// ≤•∑≈“Ù∆µºÙº≠
        ///// </summary>
        ///// <param name="clip"></param>
        ///// <param name="position"></param>
        //public void Play(AudioClip clip, Vector3 position)
        //{
        //    if (!CanPlaySoundEffect()) return;
        //    AudioSource.PlayClipAtPoint(clip, position);
        //}
        #endregion

        private void Start()
        {
            audio = GetComponent<AudioSource>();
            if (audio == null)
                audio = gameObject.AddComponent<AudioSource>();
        }

        private string GetClipNewName(string abName, string assetName)
        {
            return abName + "." + assetName;
        }
        //µ√µΩ”Œœ∑∆¨∂Œ
        private void Get(string abName, string assetName, GetBack gb)
        {
            string _key = GetClipNewName(abName, assetName);

            if (!sounds.ContainsKey(_key))
            {
                ResManager.LoadAudioClip(abName, assetName, objs =>
                {
                    if (objs.Length == 0 || objs[0] == null)
                    {
                        Debug.Log("PlayBackSound fail");
                        if (gb != null) gb(null, _key);
                        return;
                    }
                    else
                    {
                        sounds.Add(_key, objs[0] as AudioClip);
                        if (gb != null) gb(objs[0] as AudioClip, _key);
                        return;
                    }
                });
            }
            else
            {
                if (gb != null) gb(sounds[_key], _key);
            }
        }


        //≤•∑≈±≥æ∞“Ù¿÷
        public void PlayBackSound(string abName, string assetName, float volume = 1.0f)
        {
            backSoundKey = GetClipNewName(abName, assetName);
            Get(abName, assetName, (clip, key) =>
            {
                if (clip == null)
                    return;
                if (key != backSoundKey)
                    return;

                audio.loop = true;
                audio.clip = clip;
                audio.volume = volume;
                audio.Play();
            });
        }
        //Õ£÷π±≥æ∞“Ù¿÷
        public void StopBackSound()
        {
            backSoundKey = "";
            audio.volume = 0;
            audio.Stop();
            Util.ClearMemory();
        }

        //≤•∑≈“Ù–ß
        public void PlaySound(string abName, string assetName, float volume = 1.0f)
        {
            Get(abName, assetName, (clip, key) =>
            {
                if (clip == null)
                    return;
                if (Camera.main == null)
                    return;
                // AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
                var audioSource = PlayClipAtPoint(clip, Camera.main.transform.position, volume);
                dicAudios[key] = audioSource;
            });
        }
        //Õ£÷π≤•∑≈
        public void StopSound(string abName, string assetName)
        {
            string name = GetClipNewName(abName, assetName);
            if (dicAudios.ContainsKey(name))
            {
                if (dicAudios[name] != null)
                {
                    dicAudios[name].enabled = false;
                    Util.ClearMemory();
                }
            }
        }

        // <summary>
        /// ≤•∑≈“Ù∆µºÙº≠
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="position"></param>
        private AudioSource PlayClipAtPoint(AudioClip clip, Vector3 position, float volume = 1.0f)
        {
            GameObject go = new GameObject("One shot audio");
            go.transform.position = position;
            AudioSource audioSource = (AudioSource)go.AddComponent(typeof(AudioSource));
            audioSource.clip = clip;
            audioSource.spatialBlend = 1f;
            audioSource.volume = volume;
            audioSource.loop = false;
            audioSource.Play();
            UnityEngine.Object.Destroy(go, clip.length * ((Time.timeScale >= 0.01f) ? Time.timeScale : 0.01f));
            return audioSource;
        }


    }
}