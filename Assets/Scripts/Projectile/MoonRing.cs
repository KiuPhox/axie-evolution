using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRing : Projectile
{
    public float effectSpeed;

    void Update()
    {
        if (holder != null)
        {
            transform.position = holder.transform.position;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //collision.gameObject.GetComponent<Enemy>().SetSpeed(effectSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //collision.gameObject.GetComponent<Enemy>().SetOriginalSpeed();
        }
    }
}
