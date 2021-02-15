using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    private static SoundFX instance;
    public float volume;
    private AudioSource source;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            source = GetComponent<AudioSource>();
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        volume = PlayerPrefs.GetFloat("fxvolume", 50f);
    }

    public void SetVolume(float _volume)
    {
        volume = _volume;
    }

    public float GetVolume()
    {
        return volume;
    }

    public void Play(AudioClip audioClip)
    {
        source.volume = volume;
        source.clip = audioClip;
        source.Play();
    }
}
