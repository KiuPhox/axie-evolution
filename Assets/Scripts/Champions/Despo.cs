using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despo : Champion
{
    float closestDistance = 100f;
    public GameObject fiveBullets;
    public GameObject sevenBullets;

    float m_cooldownTime = 1f;

    private void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        closestTarget = GetClosestTargetInList(targets);

        if (closestTarget != null)
        {
            FlipBaseOnTargetPos(closestTarget.transform.position);
            closestDistance = Vector2.Distance(transform.position, closestTarget.transform.position);
        }

        if (currentLevel == 2)
        {
            projectile = fiveBullets;
        }else if(currentLevel == 3)
        {
            projectile = sevenBullets;
            m_cooldownTime = 0.5f;
        }

        if (Time.time >= nextAttackTime)
        {
            if (closestTarget != null && closestDistance <= championData.range)
            {
                nextAttackTime = Time.time + cooldownTime * m_cooldownTime;
                StartCoroutine(DespoShootIE(0f));
            }
        }
    }

    public void DespoShoot ()
    {
        GameObject i_projectile = Instantiate(projectile, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, damagePopupHolder.transform);
        if (i_projectile != null)
        {
            if (closestTarget != null)
            {
                i_projectile.GetComponent<Projectile>().target = closestTarget;
            }
            i_projectile.GetComponent<Projectile>().damage = damage;
            i_projectile.GetComponent<Projectile>().holder = this.gameObject;
        }
    }

    public IEnumerator DespoShootIE(float timeDelay)
    {
        skeletonAnimation.state.SetAnimation(0, championData.attackAnimation, false);
        skeletonAnimation.state.AddAnimation(0, "draft/run-origin", true, 0);
        yield return new WaitForSeconds(timeDelay);
        yield return new WaitForSeconds(0.4f);
        cameraHolder.Shake(1f);
        DespoShoot();
    }
}
