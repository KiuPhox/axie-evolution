using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityMovementAI;
public class PushAura : MonoBehaviour
{ 
    public float lifeTime;
    public float sizeScale;
    void Start()
    {
        
        transform.DOScale(new Vector3(sizeScale, sizeScale, 1), lifeTime).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            Destroy(gameObject);
        });
        
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

}
