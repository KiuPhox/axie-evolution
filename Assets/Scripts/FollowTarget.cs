using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public float speed;
    public float spaceBetween = 0.5f;
    GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 screenPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector3 targetPos = new Vector3(worldPosition.x, worldPosition.y, 0);

        if (Vector3.Distance(targetPos, transform.position) > spaceBetween)
        {
            transform.position += (targetPos - transform.position).normalized * speed * Time.fixedDeltaTime;
        }
    }


}
