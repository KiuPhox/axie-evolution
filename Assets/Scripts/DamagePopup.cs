using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] float offset;
    [SerializeField] float jumpPower;
    [SerializeField] float jumpDuration;
    void Start()
    {
        transform.position = transform.position + new Vector3(0, offset, 0);

        // Jump
        Vector3 jumpPos = new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y - 2f, transform.position.z);
        transform.DOLocalJump(jumpPos, jumpPower, 1, jumpDuration);

        // Scale font size
        GetComponent<TMP_Text>().DOFontSize(5f, 0.2f);

        //Fade text
        GetComponent<TMP_Text>().DOFade(0f, 0.1f).SetDelay(Random.Range(0.7f, 0.8f)).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
