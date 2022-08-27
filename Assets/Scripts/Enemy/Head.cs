using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityMovementAI;

public class Head : Enemy
{
    public float dashTime;
    public float chargeTime;

    float nextDashTime;

    public override void Start()
    {
        base.Start();
        nextDashTime = Time.time + Random.Range(4f, dashTime);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Time.time > nextDashTime)
        {
            nextDashTime = Time.time + dashTime;
            StartCoroutine(IEDash());
        }
    }

    IEnumerator IEDash()
    {
        SteeringBasics SB = GetComponent<SteeringBasics>();
        SB.maxVelocity = 0;
        yield return new WaitForSeconds(chargeTime);
        skeletonAnimation.state.SetAnimation(0, "action/idle/random-01", false);
        skeletonAnimation.state.AddAnimation(0, "action/move-forward", true, 0);
        yield return new WaitForSeconds(0.2f);
        SB.maxVelocity = 50;
        damage *= 1.2f;
        DOTween.To(() => SB.maxVelocity, x => SB.maxVelocity = x, 2, 0.5f).SetDelay(0.5f).OnComplete(() =>
        {
            damage /= 1.2f;
        });
    }
}
