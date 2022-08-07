using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : Projectile
{
    public float lifeTime;
    public float effectRadius;
    public float effectTime;
    public float stunTime;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            transform.position = target.transform.position;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Invoke("EffectEnemy", effectTime);

        Destroy(this.gameObject, lifeTime);
    }

    void EffectEnemy()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        if (holder.GetComponent<Champion>().currentLevel == 3)
        {
            stunTime *= 2;
        }

        foreach (GameObject t in targets)
        {
            if (Vector2.Distance(t.transform.position, transform.position) <= effectRadius)
            {
                t.GetComponent<Enemy>().stunTime = stunTime;
                t.GetComponent<LivingEntity>().TakeDamage(damage, holder.GetComponent<LivingEntity>());
                t.GetComponent<LivingEntity>().skeletonAnimation.state.SetAnimation(0, "battle/get-debuff", false);
                t.GetComponent<LivingEntity>().skeletonAnimation.state.AddAnimation(0, "draft/run-origin", false, 0f);
            }
        }
    }
}
