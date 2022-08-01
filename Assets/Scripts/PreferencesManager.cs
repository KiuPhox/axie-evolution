using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferencesManager : MonoBehaviour
{
    string audioKey = "Audio Level";
    // Start is called before the first frame update
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
        PlayerPrefs.SetFloat(audioKey, SoundManager.Instance.volumeSlider.value);
        PlayerPrefs.Save();
    }

    public void LoadPrefs()
    {
        float audioValue = PlayerPrefs.GetFloat(audioKey, 0);
        SoundManager.Instance.volumeSlider.value = audioValue;
    }
}
