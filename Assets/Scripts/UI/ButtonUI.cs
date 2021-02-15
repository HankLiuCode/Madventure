using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUI : MonoBehaviour
{
    public AudioClip clickSound;
    private SoundFX soundFX;

    private void Awake()
    {
        soundFX = GameObject.Find("SoundFX").GetComponent<SoundFX>();
    }

    public void PlaySound()
    {
        soundFX.Play(clickSound);
    }
}
