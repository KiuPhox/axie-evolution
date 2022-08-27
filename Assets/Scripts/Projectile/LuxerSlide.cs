using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LuxerSlide : MonoBehaviour
{
    SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        transform.DOScaleX(0.2f, 0.3f).SetEase(Ease.Linear);
        SR.DOFade(1f, 0.8f).SetEase(Ease.InQuart).OnComplete(() =>
        {
            SR.DOFade(0, 0.2f).SetEase(Ease.Linear);
        });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
