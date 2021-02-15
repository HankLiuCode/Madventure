using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    private BGMusic bgMusic;
    public Slider slider;

    private void Awake()
    {
        bgMusic = GameObject.Find("BGMusic").GetComponent<BGMusic>();
    }

    private void Start()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat("musicvolume", 50f);
        bgMusic.SetVolume(savedMusicVolume);
        slider.value = savedMusicVolume;
    }

    public void SetVolume(float _volume)
    {
        bgMusic.SetVolume(_volume);
        PlayerPrefs.SetFloat("musicvolume", _volume);
    }
}
