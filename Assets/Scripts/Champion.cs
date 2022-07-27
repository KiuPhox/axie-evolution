using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Champion : LivingEntity, IFollowable
{
    public float speed;
    public float spaceBetween = 0.5f;
    public ChampionData championData;
    SpriteRenderer championSR;

    void Awake()
    {
        startingHealth = championData.health;
        championSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 screenPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector3 targetPos = new Vector3(worldPosition.x, worldPosition.y, 0);

        FlipBaseOnTargetPos(targetPos);

        if (Vector3.Distance(targetPos, transform.position) > spaceBetween)
        {
            FollowTarget(targetPos);
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

    void FlipBaseOnTargetPos(Vector3 targetPos)
    {
        if (targetPos.x > transform.position.x)
        {
            championSR.flipX = true;
        }
        else
            championSR.flipX = false;
    }
}
