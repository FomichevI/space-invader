using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]

public class MonsterAudioManager : MonoBehaviour
{
    private AudioSource mainAS;

    [SerializeField] private AudioClip startRunAC;
    [SerializeField] private AudioClip deathAC;

    //только для босса
    [Header("Only for boss")]
    [SerializeField] private AudioClip spitAC;


    private void Start()
    {
        mainAS = GetComponent<AudioSource>();
        SetVolume();
    }


    public void PlayStartRun() { mainAS.PlayOneShot(startRunAC); }
    public void PlayDeath() { mainAS.PlayOneShot(deathAC); }
    public void PlaySpit() { mainAS.PlayOneShot(spitAC); }

    public void SetVolume()
    {
        mainAS.volume = SaveController.GetSoundVolume();
    }
}
