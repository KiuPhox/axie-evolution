using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExploderBullet : MonoBehaviour
{
    public ChampionData championData;
    public float speed;
    public float moveTime;

    float damage;
    private void Start()
    {
        transform.DOScaleX(0.25f, 0.1f).SetEase(Ease.Linear).SetLoops(4, LoopType.Yoyo);
        moveTime += Time.time;
        Destroy(gameObject, 4f);
        damage = championData.damage + 4 * (GameManager.Instance.currentLevel - 1);
    }

    private void Update()
    {
        if (Time.time > moveTime)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Champion"))
        {
            collision.GetComponent<LivingEntity>().TakeDamage(damage, null);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
