using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : Projectile
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(-60, 0, 0);
    }
    void Update()
    {
        if (target != null) { 
            transform.position = target.transform.position; 
        }   
    }
}
