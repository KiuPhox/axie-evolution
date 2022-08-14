using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferencesManager : MonoBehaviour
{
    string audioKey = "Audio Level";

    void Start()
    {
        LoadPrefs();
    }

    private void OnApplicationQuit()
    {
        SavePrefs();
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetInt(audioKey, SoundManager.Instance.volumeSlider.volumeValue);
        PlayerPrefs.Save();
    }

    public void LoadPrefs()
    {
        int audioValue = PlayerPrefs.GetInt(audioKey, 0);
        SoundManager.Instance.volumeSlider.volumeValue = audioValue;
    }
}
