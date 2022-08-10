using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PoisonSlash : Projectile
{
    public Ease ease;
    public float lifeTime;
    public float distance;
    public float effectTime;

    Vector3 targetPos;
    Vector3 direction;

    int currentLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = holder.GetComponent<Champion>().currentLevel;
        if (target != null)
        {
            targetPos = target.transform.position;
            direction = targetPos - transform.position + new Vector3(0f, 0.5f, 0f);
            RotateToDirection(direction);
            transform.DOMove(transform.position + direction.normalized * distance, lifeTime).SetEase(ease).OnComplete(() =>
            {
                GetComponent<BoxCollider2D>().enabled = false;
            });
            if (currentLevel == 3)
            {
                transform.localScale = new Vector3(0.8f, 0.8f, 1);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject, lifeTime + 5f);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().isPoision = true;
            for (int i = 0; i < 5 * currentLevel; i++)
            {
                StartCoroutine(PosionTarget(collision.gameObject, effectTime * i));
            }   
        }
    }

    IEnumerator PosionTarget(GameObject target, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        if (!target.activeSelf)
        {
            target.GetComponent<Enemy>().isPoision = false;
        }
        if (target.GetComponent<Enemy>().isPoision && target.activeSelf)
        {
            target.GetComponent<Enemy>().TakeDamage(damage, holder.GetComponent<LivingEntity>());
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
