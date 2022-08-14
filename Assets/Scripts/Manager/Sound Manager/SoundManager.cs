using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public VolumeSlider volumeSlider;

    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource healEffectSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeMasterVolume(volumeSlider.volumeValue);
    }

    public void PlaySound (AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }

    public void PlayHealSound()
    {
        healEffectSource.Play();
    }


    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value / 5f;
    }
}
