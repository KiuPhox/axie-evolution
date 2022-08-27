using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boomerang : Projectile
{
    public float currentSpeed;
    public float effectTime;
    public float rotationTime;

    private float originalSpeed;
    Vector3 targetPos;
    Vector3 direction;

    LivingEntity targetEntity;

    void Start()
    {
        effectTime += Time.time;
        originalSpeed = currentSpeed;
        transform.DOLocalRotate(new Vector3(0, 0, 360), rotationTime, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental);

        if (target != null)
        {
            targetPos = target.transform.position;
            direction = targetPos - transform.position;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        transform.Translate(direction.normalized * currentSpeed * Time.deltaTime, Space.World);
       
        if (!holder.activeSelf)
        {
            Destroy(this.gameObject);
        }
        else if (Time.time >= effectTime) 
        {
            direction = holder.transform.position - transform.position;
            currentSpeed = Mathf.Lerp(currentSpeed, originalSpeed, 0.005f);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, 0.005f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Enemy"))
        {
            targetEntity = collision.gameObject.GetComponent<LivingEntity>();
            targetEntity.TakeDamage(damage, holder.GetComponent<LivingEntity>());
            if (Time.time < effectTime)
            {
                currentSpeed -= 1f;
            }
        }
        if (Time.time >= effectTime && collision.gameObject == holder)
        {
            Destroy(this.gameObject);
        }    
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
