using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    private static BGMusic instance;
    private AudioSource bgAudio;
    public float volume;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            bgAudio = GetComponent<AudioSource>();
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        bgAudio.volume = PlayerPrefs.GetFloat("musicvolume", 50f);
    }

    public void SetVolume(float _volume)
    {
        volume = _volume;
        bgAudio.volume = volume;
    }

    public float GetVolume()
    {
        return volume;
    }


}
