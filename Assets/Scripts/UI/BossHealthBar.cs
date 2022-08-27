using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public GameObject bossHealth;

    Image bossHealth_i;
    private void Start()
    {
        bossHealth_i = bossHealth.GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        GameObject boss = GameObject.Find("Skud(Clone)");
        if (boss != null)
        {
            Enemy boss_e = boss.GetComponent<Enemy>();
            bossHealth_i.fillAmount = boss_e.health / boss_e.maxHealth;
        }
    }
}
