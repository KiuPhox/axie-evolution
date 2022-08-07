using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noah : Champion
{
    bool isHeal = false;

    private void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Champion");

        if (Time.time >= nextAttackTime)
        {
            isHeal = false;
            nextAttackTime = Time.time + cooldownTime;
            foreach (GameObject t in targets)
            {
                float maxHealth = t.GetComponent<LivingEntity>().maxHealth;
                if (t.GetComponent<LivingEntity>().health / maxHealth <= 0.5 && !isHeal)
                {
                    if (currentLevel != 3)
                    {
                        isHeal = true;
                    }
                    Debug.Log("Heal " + t.name);
                    StartCoroutine(HealIE(t));
                    t.GetComponent<LivingEntity>().health += maxHealth * 0.2f;
                }
            }
        }

    }
    public void Heal(GameObject targetToHeal)
    {
        GameObject i_projectile = Instantiate(projectile, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, damagePopupHolder.transform);
        if (i_projectile != null)
        {
            if (targetToHeal != null)
            {
                i_projectile.GetComponent<Projectile>().target = targetToHeal;
            }
            i_projectile.GetComponent<Projectile>().damage = damage;
            i_projectile.GetComponent<Projectile>().holder = this.gameObject;
        }
    }
    public IEnumerator HealIE(GameObject targetToHeal)
    {
        skeletonAnimation.state.SetAnimation(0, championData.attackAnimation, false);
        skeletonAnimation.state.AddAnimation(0, "draft/run-origin", true, 0);
        yield return new WaitForSeconds(0.4f);
        Heal(targetToHeal);
    }
}
