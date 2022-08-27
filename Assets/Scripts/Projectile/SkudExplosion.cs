using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkudExplosion : Projectile
{
    public GameObject skud_VFX;

    SpriteRenderer SR;
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        transform.DOScale(1f, 1f).SetEase(Ease.Linear);
        transform.DOLocalRotate(new Vector3(0, 0, 160f), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
        SR.DOFade(1f, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            GameObject.Find("Camera Holder").GetComponent<CameraHolder>().Shake(5f);
            skud_VFX.SetActive(true);
            SR.DOFade(0, 0.2f).SetEase(Ease.Linear);
            GameObject[] champions = GameObject.FindGameObjectsWithTag("Champion");
            foreach (GameObject champion in champions)
            {
                if (champion.activeSelf && Vector2.Distance(champion.transform.position, transform.position) < 2.5f)
                {
                    champion.GetComponent<LivingEntity>().TakeDamage(damage, null);
                }
            }
        });

        Destroy(gameObject, 2.1f);
    }

    private void OnDestroy()
    {
        SR.DOKill();
    }
}
