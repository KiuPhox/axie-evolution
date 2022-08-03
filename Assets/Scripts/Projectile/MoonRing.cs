using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRing : Projectile
{
    public float effectRadius;

    void Update()
    {
        transform.position = holder.transform.position;
    }
}
