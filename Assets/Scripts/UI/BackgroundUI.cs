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
    int spriteIndex = 1;
    private void Awake()
    {
        nextFadeTime = 0f;
        nextFadeTime = Time.time + fadeBetweenTime;
        firstBg.GetComponent<RectTransform>().DOScale(1.1f, fadeBetweenTime).SetEase(Ease.Linear);
        firstBg.DOFade(0f, fadeDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            firstBg.GetComponent<RectTransform>().DOScale(1f, 0f).SetEase(Ease.Linear);
        });
        secondBg.GetComponent<RectTransform>().DOScale(1f, 0f).SetEase(Ease.Linear);
        secondBg.GetComponent<RectTransform>().DOScale(1.1f, fadeBetweenTime).SetEase(Ease.Linear);
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

            firstBg.GetComponent<RectTransform>().DOScale(1.1f, fadeBetweenTime).SetEase(Ease.Linear);
            firstBg.DOFade(0f, fadeDuration).SetEase(Ease.Linear).OnComplete(() =>
            {
                firstBg.GetComponent<RectTransform>().DOScale(1f, 0f).SetEase(Ease.Linear);
            });
            secondBg.GetComponent<RectTransform>().DOScale(1f, 0f).SetEase(Ease.Linear);
            secondBg.GetComponent<RectTransform>().DOScale(1.1f, fadeBetweenTime).SetEase(Ease.Linear);
        }
    }

    private void OnDestroy()
    {
        firstBg.GetComponent<RectTransform>().DOKill();
        secondBg.GetComponent<RectTransform>().DOKill();
        firstBg.DOKill();
    }
}
