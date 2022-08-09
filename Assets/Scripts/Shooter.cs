using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    private void Update()
    {
        closestChampion = GetClosestTargetInList(playerChampions.champions);
        FlipBaseOnTargetPos(closestChampion.transform.position);
        if (Time.time > nextAttackTime && !isStunned)
        {
            nextAttackTime = Time.time + attackCooldownTime;
            StartCoroutine(ShootIE(0f));
            StartCoroutine(ShootIE(0.15f));
            StartCoroutine(ShootIE(0.3f));
        }
    }

    IEnumerator ShootIE(float timeDelay)
    {
        skeletonAnimation.state.SetAnimation(0, championData.attackAnimation, false);
        skeletonAnimation.state.AddAnimation(0, "draft/run-origin", true, 0);
        yield return new WaitForSeconds(timeDelay);
        yield return new WaitForSeconds(0.5f);
        Shoot();
    }

    void Shoot()
    {
        GameObject i_projectile = Instantiate(projectile, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, damagePopupHolder.transform);
        if (i_projectile != null)
        {
            if (closestChampion != null)
            {
                i_projectile.GetComponent<Projectile>().target = closestChampion;
            }
            i_projectile.GetComponent<Projectile>().damage = damage;
            i_projectile.GetComponent<Projectile>().holder = this.gameObject;
        }
    }
}
