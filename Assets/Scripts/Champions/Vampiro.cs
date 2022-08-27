using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampiro : Champion
{
    bool isHeal = false;
    float healPercent = 0.3f;
    Vector3 offset = new Vector3(0, 0.5f, 0);

    List<GameObject> enemyPool = new List<GameObject>();
    private new void Awake()
    {
        base.Awake();
        enemyPool = ObjectPooler.Instance.GetEnemyPool();
    }

    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] champions = GameObject.FindGameObjectsWithTag("Champion");

        for (int i = 0; i < enemyPool.Count; i++)
        {
            GameObject bloodline = ObjectPooler.Instance.GetPooledObjectIndex("bloodline", i);
            if (enemyPool[i].activeSelf)
            {
                if (Vector2.Distance(enemyPool[i].transform.position, transform.position) <= championData.range)
                {
                    if (enemyPool[i].activeSelf)
                    {
                        bloodline.SetActive(true);
                        bloodline.GetComponent<LineRenderer>().SetPosition(1, transform.position + offset);
                        bloodline.GetComponent<LineRenderer>().SetPosition(0, enemyPool[i].transform.position + offset);
                    }

                }
                else
                {
                    bloodline.SetActive(false);
                }
            }
            else
            {
                bloodline.SetActive(false);
            }
        }



        if (Time.time >= nextAttackTime)
        {
            foreach (GameObject enemy in enemies)
            {
                isHeal = false;

                if (Vector2.Distance(enemy.transform.position, transform.position) <= championData.range)
                {
                    nextAttackTime = Time.time + cooldownTime;
                    enemy.GetComponent<LivingEntity>().TakeDamage(damage, this);
                    while (!isHeal)
                    {
                        GameObject champion = champions[Random.Range(0, champions.Length)];
                        if (champion.activeSelf) {
                            isHeal = true;
                            StartCoroutine(HealIE(champion));
                            if (currentLevel == 3)
                            {
                                healPercent = 0.5f;
                            }
                            champion.GetComponent<LivingEntity>().Healing(damage * healPercent);
                        }
                    }
                }
            }
        }
    }
    private IEnumerator HealIE(GameObject targetToHeal)
    {
        skeletonAnimation.state.SetAnimation(0, championData.attackAnimation, false);
        skeletonAnimation.state.AddAnimation(0, "draft/run-origin", true, 0);
        yield return new WaitForSeconds(0.4f);
    }
}
