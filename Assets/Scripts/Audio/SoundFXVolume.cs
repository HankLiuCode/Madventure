using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundFXVolume : MonoBehaviour
{
    private SoundFX soundFX;
    public Slider slider;

    private void Awake()
    {
        soundFX = GameObject.Find("SoundFX").GetComponent<SoundFX>();
    }

    private void Start()
    {
        float savedFXVolume = PlayerPrefs.GetFloat("fxvolume", 50f);
        soundFX.SetVolume(savedFXVolume);
        slider.value = savedFXVolume;
    }

    public void SetVolume(float _volume)
    {
        soundFX.SetVolume(_volume);
        PlayerPrefs.SetFloat("fxvolume", _volume);
    }
}
