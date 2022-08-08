using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraHolder : MonoBehaviour
{
    [SerializeField] float positionStrength;
    [SerializeField] float rotationStrength;
    [SerializeField] float shakeDuration;
    [SerializeField] float delay;

    public void Shake()
    {
        transform.DOShakePosition(shakeDuration, positionStrength, fadeOut: true).SetDelay(delay);
        transform.DOShakeRotation(shakeDuration, rotationStrength, fadeOut: true).SetDelay(delay);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
