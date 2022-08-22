using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public VolumeSlider volumeSlider;
    public List<AudioClip> deathClips = new List<AudioClip>();

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
        effectsSource.pitch = Random.Range(0.95f, 1.05f);
        effectsSource.PlayOneShot(clip);
    }

    public void PlayDeathSound()
    {
        effectsSource.pitch = Random.Range(0.9f, 1.1f);
        effectsSource.PlayOneShot(Utility.RandomPick(deathClips));
    }

    public void PlayHealSound()
    {
        effectsSource.pitch = Random.Range(0.95f, 1.05f);
        healEffectSource.Play();
    }


    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value / 5f;
    }
}
