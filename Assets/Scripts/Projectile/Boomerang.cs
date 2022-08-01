using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boomerang : Projectile
{
    public float speed;
    public float lifeTime;
    public float effectTime;
    public float rotateTime;

    private float originSpeed;
    Vector3 targetPos;
    Vector3 direction;

    LivingEntity targetEntity;
    float nextEffectTime;

    void Start()
    {
        effectTime += Time.time;
        originSpeed = speed;
        transform.DOLocalRotate(new Vector3(0, 0, 360), rotateTime, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental);
        // t biet bomerang lay cai gì r :)) bumerang trong gunny :))
        // voi cai thượng cổ nữa :))
        if (target != null)
        {
            targetPos = target.transform.position;
            direction = targetPos - transform.position;
        }
        else // đù
        {
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject, lifeTime);
    }
    // đó nó mát tiep

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World); // chạy theo hướng, 
       
        if (Time.time >= effectTime) 
        {
            direction = holder.transform.position - transform.position;
            speed = originSpeed;
        }
        // nếu dị thì cái này back nó true mãi r
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Enemy"))
        {
            // Deals target damage
            targetEntity = collision.gameObject.GetComponent<LivingEntity>();
            targetEntity.TakeDamage(damage);
            speed -= 1f;
        }
        if (Time.time >= effectTime && collision.gameObject == holder)
        {
            Destroy(this.gameObject);
        }    
    }
}
