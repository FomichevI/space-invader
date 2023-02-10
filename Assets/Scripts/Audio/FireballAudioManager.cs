using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireballAudioManager : MonoBehaviour
{
    private AudioSource mainAS;

    private void Start()
    {
        mainAS = GetComponent<AudioSource>();
        SetVolume();
    }

    public void PlayBoom() { mainAS.Play(); }

    public void SetVolume()
    {
        mainAS.volume = SaveController.GetSoundVolume();
    }
}
