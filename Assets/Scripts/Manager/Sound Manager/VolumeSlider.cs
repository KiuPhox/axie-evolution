using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Sprite[] volumeSprites;

    public int volumeValue;
    Image volumeImage;

    void Start()
    {
        volumeImage = GetComponent<Image>();
        ChangeVolumeSprite(volumeValue);
    }

    public void IncreaseVolume()
    {
        if (volumeValue < 5)
        {
            volumeValue++;
            ChangeVolumeSprite(volumeValue);
            SoundManager.Instance.ChangeMasterVolume(volumeValue);
        }
    }

    public void DecreaseVolume()
    {
        if (volumeValue > 0)
        {
            volumeValue--;
            ChangeVolumeSprite(volumeValue);
            SoundManager.Instance.ChangeMasterVolume(volumeValue);
        }
    }

    private void ChangeVolumeSprite(int volumeValue)
    {
        volumeImage.sprite = volumeSprites[volumeValue];
    }
}
