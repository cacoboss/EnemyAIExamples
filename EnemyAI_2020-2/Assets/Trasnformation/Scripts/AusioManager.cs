using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AusioManager : MonoBehaviour
{
    #region Static Instances
    private static AusioManager _f_instance;        //Field
    public static AusioManager p_Instance           //Property
    {
        get
        {
            if (_f_instance == null)
            {
                _f_instance = FindObjectOfType<AusioManager>();
                if (_f_instance == null)
                {
                    _f_instance = new GameObject("Spawned Audio Manager", typeof(AusioManager))
                        .GetComponent<AusioManager>();
                }
            }

            return _f_instance;
        }
        private set
        {
            _f_instance = value;
        }
    }
    #endregion

    #region Fields
    private AudioSource _musicSource_01;
    private AudioSource _musicSource_02;
    private AudioSource _sfxSource;

    [SerializeField] private bool _firstMusicSourceIsPlaying;
    #endregion
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        _musicSource_01 = this.gameObject.AddComponent<AudioSource>();
        _musicSource_02 = this.gameObject.AddComponent<AudioSource>();
        _sfxSource = this.gameObject.AddComponent<AudioSource>();

        _musicSource_01.loop = true;
        _musicSource_02.loop = true;
        

        _firstMusicSourceIsPlaying = true;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        AudioSource activeSource = (_firstMusicSourceIsPlaying) ? _musicSource_01 : _musicSource_02;
        
        activeSource.clip = musicClip;
        activeSource.volume = 1f;
        activeSource.Play();
    }
    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        AudioSource activeSource = (_firstMusicSourceIsPlaying) ? _musicSource_01 : _musicSource_02;
        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }
    public void PlayMusicWithCrossfade(AudioClip musicClip, float transitionTime = 1f)
    {
        AudioSource activeSource = (_firstMusicSourceIsPlaying) ? _musicSource_01 : _musicSource_02;
        AudioSource newSource = (_firstMusicSourceIsPlaying) ? _musicSource_02 : _musicSource_01;

        _firstMusicSourceIsPlaying = !_firstMusicSourceIsPlaying;

        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
    }

    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
        float f = 0f;

        for (f = 0f; f <= transitionTime; f+= Time.deltaTime)
        {
            original.volume = (1 - (f / transitionTime));
            newSource.volume = (f / transitionTime);
            yield return null;
        }
        original.Stop();
    }
    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        if (!activeSource.isPlaying)
        {
            activeSource.Play();
        }

        float t = 0f;

        for ( t = 0f; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (1 - (t / transitionTime));
            yield return null;
        }
        
        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();
        
        for ( t = 0f; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume =  (t / transitionTime);
            yield return null;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }
    public void PlaySFX(AudioClip clip, float volume)
    {
        _sfxSource.PlayOneShot(clip, volume);
    }

    public void StopMusic()
    {
        _musicSource_01.Stop();
        _musicSource_02.Stop();
    }
}
