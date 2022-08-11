using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderBullet : MonoBehaviour
{
    public float speed;
    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
    }
}
