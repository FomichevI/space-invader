
using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager S;
    [SerializeField] private AudioClip clickAC;
    [SerializeField] private AudioClip completeAC;
    [SerializeField] private AudioClip dangerAC;
    [SerializeField] private AudioClip healAC;
    [SerializeField] private AudioClip rechargeAC;

    [SerializeField] private AudioSource musicAS;
    [SerializeField] private AudioSource mainAS;

    private void OnEnable()
    {
        EventAggregator.AddBullets.AddListener(PlayRecharge);
        EventAggregator.HealPlayer.AddListener(PlayHeal);
    }
    private void OnDisable()
    {
        EventAggregator.AddBullets.RemoveListener(PlayRecharge);
        EventAggregator.HealPlayer.RemoveListener(PlayHeal);

    }
    private void Start()
    {
        DontDestroyOnLoad(transform.parent.gameObject);
        if (S == null)
            S = this;
        else
            Destroy(transform.parent.gameObject);
        SetVolume();
    }


    public void SetVolume()
    {
        mainAS.volume = SaveController.GetSoundVolume();
        musicAS.volume = SaveController.GetMusicVolume();
    }

    public void PlayClick()    {mainAS.PlayOneShot(clickAC);}
    public void PlayComplete() { mainAS.PlayOneShot(completeAC); }
    public void PlayDanger() { mainAS.PlayOneShot(dangerAC); }
    public void PlayHeal() { mainAS.PlayOneShot(healAC); }
    public void PlayRecharge() { mainAS.PlayOneShot(rechargeAC); }
}
