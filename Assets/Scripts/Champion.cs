using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Champion : LivingEntity, IFollowable
{
    public float speed;
    public float spaceBetween = 0.5f;

    float nextAttackTime;

    public override void Start()
    {
        base.Start();
        nextAttackTime = Random.Range(Time.time, Time.time + cooldownTime);
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {    
            nextAttackTime = Time.time + cooldownTime;
            Shoot();
        }
    }

    void FixedUpdate()
    {
        
        Vector3 targetPos = Utility.GetMouseWorldPosition();

        FlipBaseOnTargetPos(targetPos);

        if (GameManager.Instance.State == GameState.GameStart)
        {
            if (Vector3.Distance(targetPos, transform.position) > spaceBetween)
            {
                FollowTarget(targetPos);
            }
        }
    }

    void OnNewWave(int waveNumber)
    {
        health = startingHealth;
    }

    public void FollowTarget(Vector3 targetPos)
    {
        transform.position += (targetPos - transform.position).normalized * speed * Time.fixedDeltaTime;
    }

    

    private void Shoot()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject i_projectile = Instantiate(projectile, transform.position, Quaternion.identity);

        if (i_projectile != null) {
            //Please Fix
            i_projectile.GetComponent<Projectile>().target = GetClosestTargetInList(targets);
            i_projectile.GetComponent<Projectile>().damage = damage;
        }
    }
}
