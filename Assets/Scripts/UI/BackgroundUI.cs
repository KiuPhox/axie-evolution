using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BackgroundUI : MonoBehaviour
{
    public Image firstBg;
    public Image secondBg;

    public List<Sprite> bgSprites = new List<Sprite>();

    public float fadeDuration;
    public float fadeBetweenTime;

    float nextFadeTime;
    int spriteIndex = 0;
    private void Start()
    {
        nextFadeTime = Time.time + fadeBetweenTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFadeTime)
        {
            spriteIndex++;
            if (spriteIndex == bgSprites.Count)
            {
                spriteIndex = 0;
            }
            nextFadeTime = Time.time + fadeBetweenTime;
            
            firstBg.color = Color.white;
            firstBg.sprite = secondBg.sprite;
            secondBg.sprite = bgSprites[spriteIndex];
            firstBg.DOFade(0f, fadeDuration);
            secondBg.GetComponent<RectTransform>().DOScale(1f, 0f).OnComplete(() =>
            {
                secondBg.GetComponent<RectTransform>().DOScale(1.1f, fadeBetweenTime).SetEase(Ease.Linear);
            });
            
        }
    }
}
