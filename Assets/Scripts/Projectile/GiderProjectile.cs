using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GiderProjectile : Projectile
{
    public float lifeTime;
    public GameObject giderFrame;
    public GameObject giderGold;

    void Start()
    {
        transform.position = holder.transform.position;
        Vector3 direction = target.transform.position - holder.transform.position;
        RotateToDirection(direction);
        giderFrame.transform.DOScaleY(0.15f, 0.2f);
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        giderFrame.transform.DOKill();
    }
}
