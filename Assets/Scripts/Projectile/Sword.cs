using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sword : Projectile
{
    public float angle;
    public float rotateDuration;
    public Ease ease;
    public float lifeTime;

    Vector3 targetPos;
    Vector3 direction;

    LivingEntity targetEntity;

    void Start()
    {
        if (target != null)
        {
            targetPos = target.transform.position;
            direction = targetPos - transform.position;
            RotateToDirection(direction);



            
            transform.Rotate(new Vector3(0, 0, -angle), Space.Self);
            transform.DOLocalRotate(new Vector3(0, 0, angle * 2), rotateDuration, RotateMode.LocalAxisAdd).SetEase(ease);
            
        }
        else
        {
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = holder.transform.position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            targetEntity = collision.gameObject.GetComponent<LivingEntity>();
            targetEntity.TakeDamage(damage, holder.GetComponent<LivingEntity>());
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
