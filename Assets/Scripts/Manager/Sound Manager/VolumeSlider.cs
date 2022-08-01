using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Start()
    {
        SoundManager.Instance.ChangeMasterVolume(slider.value / slider.maxValue);
        slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMasterVolume(val / slider.maxValue));
    }
}
