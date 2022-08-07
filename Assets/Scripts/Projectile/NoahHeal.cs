using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoahHeal : Projectile
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(-60, 0, 0);
    }
    void Update()
    {
        transform.position = target.transform.position;
    }
}
