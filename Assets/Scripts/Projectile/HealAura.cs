using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAura : Projectile
{
    public float lifeTime;
    public float effectTime;
    public float effectRange;

    float nextEffectTime;
    void Start()
    {
        transform.position = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-4f, 4f));
        lifeTime += Time.time;
        if (holder.GetComponent<Champion>().currentLevel == 3)
        {
            transform.localScale = new Vector3(transform.localScale.x * 2, transform.localScale.y * 2, transform.localScale.z);
            effectTime /= 2;
            effectRange *= 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextEffectTime)
        {
            List<GameObject> targetsInRange = new List<GameObject>();
            nextEffectTime = Time.time + effectTime;
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Champion");

            foreach (GameObject t in targets)
            {
                if (t.activeSelf && Vector2.Distance(t.transform.position, transform.position) <= effectRange)
                {
                    targetsInRange.Add(t);
                }
            }

            if (targetsInRange.Count > 0) { 
                int randomIndex = Random.Range(0, targetsInRange.Count);
                HealingTarget(targetsInRange[randomIndex]);

                if (holder.GetComponent<Champion>().currentLevel == 3)
                {
                    randomIndex = Random.Range(0, targetsInRange.Count);
                    HealingTarget(targetsInRange[randomIndex]);
                }
            }
        }

        if (Time.time > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void HealingTarget(GameObject targetToHeal)
    {
        float maxHealth = targetToHeal.GetComponent<LivingEntity>().maxHealth;
        targetToHeal.GetComponent<LivingEntity>().Healing(maxHealth * 0.2f);
    }
}
