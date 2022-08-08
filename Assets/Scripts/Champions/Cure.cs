using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cure : Champion
{ 
    private void Update()
    {
        if (GameManager.Instance.State == GameState.GameStart)
        {
            if (Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + cooldownTime;
                closestTarget = this.gameObject;
                StartCoroutine(ShootIE(0f));
            }
        }
    }
}
