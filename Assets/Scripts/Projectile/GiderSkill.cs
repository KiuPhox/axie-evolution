using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GiderSkill : Projectile
{
    public float lifeTime;
    public GameObject giderFrame;
    public GameObject giderProjectile;
    public float speed;
    public float giderProjectileDelay;

    SpriteRenderer giderFrameSR;

    void Start()
    {
        transform.position = holder.transform.position;
        Vector3 direction = target.transform.position - holder.transform.position;
        RotateToDirection(direction);

        giderFrameSR = giderFrame.GetComponent<SpriteRenderer>();
        giderFrame.transform.DOScaleY(0.15f, 0.2f);
        giderFrameSR.DOFade(0, 0.5f).SetDelay(0.8f);

        giderProjectileDelay += Time.time;
        giderProjectile.GetComponent<Projectile>().damage = damage;
        giderProjectile.GetComponent<Projectile>().holder = holder;
        Destroy(gameObject, lifeTime);

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > giderProjectileDelay)
        {
            if (giderProjectile != null)
            {
                giderProjectile.SetActive(true);
                giderProjectile.transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
            }
        }
    }
    private void OnDestroy()
    {
        giderFrame.transform.DOKill();
        giderFrameSR.DOKill();
    }
}
