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
            foreach (GameObject t in targets)
            {
                float maxHealth = t.GetComponent<LivingEntity>().maxHealth;
                if (t.activeSelf && t.GetComponent<LivingEntity>().health / maxHealth <= 0.5 && !isHeal)
                {
                    if (currentLevel != 3)
                    {
                        isHeal = true;
                    }
                    nextAttackTime = Time.time + cooldownTime;
                    StartCoroutine(HealIE(t));
                    t.GetComponent<LivingEntity>().Healing(maxHealth * 0.2f);
                }
            }
        }

    }

    private IEnumerator HealIE(GameObject targetToHeal)
    {
        skeletonAnimation.state.SetAnimation(0, championData.attackAnimation, false);
        skeletonAnimation.state.AddAnimation(0, "draft/run-origin", true, 0);
        yield return new WaitForSeconds(0.4f);
    }
}
