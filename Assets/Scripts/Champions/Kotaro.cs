using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kotaro : Champion
{
    float closestDistance = 100f;
    public GameObject critSlash;
    public GameObject normalSlash;

    int attack = 0;
    int damageMutiply = 2;

    private void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        closestTarget = GetClosestTargetInList(targets);

        if (closestTarget != null)
        {
            FlipBaseOnTargetPos(closestTarget.transform.position);
            closestDistance = Vector2.Distance(transform.position, closestTarget.transform.position);
        }

        if (Time.time >= nextAttackTime)
        {
            if (closestTarget != null && closestDistance <= championData.range)
            {
                nextAttackTime = Time.time + cooldownTime;
                
                projectile = normalSlash;
                damageMutiply = 1;
                attack++;

                if (attack == 3)
                {
                    attack = 0;
                    if (currentLevel == 3 || Utility.RandomBool(50))
                    {
                        projectile = critSlash;
                        damageMutiply = 2;
                        if (currentLevel == 3)
                            damageMutiply = 3;
                    }
                }
                StartCoroutine(SlashIE(0f));
            }
        }
    }

    public void Slash ()
    {
        GameObject i_projectile = Instantiate(projectile, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, damagePopupHolder.transform);
        if (i_projectile != null)
        {
            if (closestTarget != null)
            {
                i_projectile.GetComponent<Projectile>().target = closestTarget;
            }
            
            i_projectile.GetComponent<Projectile>().damage = damage * damageMutiply;
            i_projectile.GetComponent<Projectile>().holder = this.gameObject;
        }
    }

    public IEnumerator SlashIE(float timeDelay)
    {
        skeletonAnimation.state.SetAnimation(0, championData.attackAnimation, false);
        skeletonAnimation.state.AddAnimation(0, "draft/run-origin", true, 0);
        yield return new WaitForSeconds(timeDelay);
        yield return new WaitForSeconds(0.4f);
        Slash();
    }
}
