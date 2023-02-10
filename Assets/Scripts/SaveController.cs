
using UnityEngine;

public static class SaveController
{
    public static void SetSoundVolume(float volume)
    {
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }
    public static void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public static float GetSoundVolume()
    {
        if (PlayerPrefs.HasKey("SoundVolume"))
            return (PlayerPrefs.GetFloat("SoundVolume"));
        else
            return 1;
    }
    public static float GetMusicVolume()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
            return (PlayerPrefs.GetFloat("MusicVolume"));
        else
            return 1;
    }   
    public static void SetMaxScore(int score)
    {
        PlayerPrefs.SetInt("MaxScore", score);
    }
    public static float GetMaxScore()
    {
        return (PlayerPrefs.GetInt("MaxScore"));
    }
}
