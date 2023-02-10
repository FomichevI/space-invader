using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIPanelsController : MonoBehaviour
{
    public static UIPanelsController S;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private TextMeshProUGUI yourScoreText;
    [SerializeField] private TextMeshProUGUI hightScoreText;

    [SerializeField] private float timeToShowHint = 1;


    void Start()
    {
        if (S == null)
            S = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ShowPausePanel();
    }

    public void ShowNewWaveText(int waveIndex)
    {
        hintText.text = "Wave " + waveIndex;
        hintText.gameObject.SetActive(true);
        Invoke("HideNewWaveText", timeToShowHint);
    }

    public void ShowBossText()
    {
        hintText.text = "Boss!";
        hintText.gameObject.SetActive(true);
        Invoke("HideNewWaveText", timeToShowHint);
    }

    private void HideNewWaveText()
    {
        hintText.gameObject.SetActive(false);
    }


    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        yourScoreText.text = "Your Score: " + Counter.S.count.ToString();

        if (SaveController.GetMaxScore() >= Counter.S.count)
            hightScoreText.text = "Hight Score: " + SaveController.GetMaxScore();
        else
        {
            SaveController.SetMaxScore(Counter.S.count);
            hightScoreText.text = "New Hight Score!";
        }
        Time.timeScale = 0;
    }

    public void ShowPausePanel()
    {
        UIAudioManager.S.PlayClick();
        PlayerInput.S.canMove = false;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ContinuePlay()
    {
        UIAudioManager.S.PlayClick();
        PlayerInput.S.canMove = true;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void RastartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
