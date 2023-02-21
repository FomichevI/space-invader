using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]

public class PlayerAudioManager : MonoBehaviour
{
    private AudioSource mainAS;
    [SerializeField] private AudioSource stepAS;

    [SerializeField] private AudioClip getDamageAC;
    [SerializeField] private AudioClip deathAC;
    [SerializeField] private AudioClip pistolAC;
    [SerializeField] private AudioClip rifleAC;
    [SerializeField] private AudioClip shotgunAC;
    [SerializeField] private AudioClip changeWeaponAC;
    [SerializeField] private AudioClip misfireAC;


    private void Start()
    {
        mainAS = GetComponent<AudioSource>();
        SetVolume();        
    }

    public void PlayMove()
    {
        stepAS.enabled = true;
    }
    public void StopMove()
    {
        stepAS.enabled = false;
    }

    public void PlayPistol()    { mainAS.PlayOneShot(pistolAC); }
    public void PlayRifle() { mainAS.PlayOneShot(rifleAC); }
    public void PlayShotgun() { mainAS.PlayOneShot(shotgunAC); }
    public void PlayGetDamage() { mainAS.PlayOneShot(getDamageAC); }
    public void PlayDeath() { mainAS.PlayOneShot(deathAC); }
    public void PlayChangeWeapon() { mainAS.PlayOneShot(changeWeaponAC); }
    public void PlayMisfire() { if(!mainAS.isPlaying) mainAS.PlayOneShot(misfireAC); }

    public void SetVolume()
    {
        mainAS.volume = SaveController.GetSoundVolume();
        stepAS.volume = SaveController.GetSoundVolume();
    }
}
