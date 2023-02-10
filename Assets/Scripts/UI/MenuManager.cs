using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject wantToQuitPanel;

    [SerializeField] private Slider soundClider;
    [SerializeField] private Slider musicSlider;

    [SerializeField] private Image progressImg;

    private void Start()
    {
        soundClider.value = SaveController.GetSoundVolume();
        musicSlider.value = SaveController.GetMusicVolume();
    }

    public void OpenSettingsPanel()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
        UIAudioManager.S.PlayClick();
    }

    public void OpenMenuPanel()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        wantToQuitPanel.SetActive(false);
        UIAudioManager.S.PlayClick();
    }

    public void OpenLoadingPanel()
    {
        mainPanel.SetActive(false);
        loadingPanel.SetActive(true);
        UIAudioManager.S.PlayClick();
        LoadGameplay();
    }

    public void OpenWantToQuitPanel()
    {
        mainPanel.SetActive(false);
        wantToQuitPanel.SetActive(true);
        UIAudioManager.S.PlayClick();
    }

    public void ChangeSoundVolume()
    {
        SaveController.SetSoundVolume(soundClider.value);
        UIAudioManager.S.SetVolume();
    }
    public void ChangeMusicVolume()
    {
        SaveController.SetMusicVolume(musicSlider.value);
        UIAudioManager.S.SetVolume();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadGameplay()
    {
        StartCoroutine(LoadingFunction());
    }

    IEnumerator LoadingFunction()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        while (!operation.isDone)
        {
            float progress = operation.progress;
            progressImg.fillAmount = progress;
            yield return null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenMenuPanel();
    }

}
