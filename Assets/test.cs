using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AxieMixer.Unity;
using Spine.Unity;

public class test : MonoBehaviour
{
    public string axieId;
    public string genesString;
    SkeletonAnimation skeletonAnimation;
    // Start is called before the first frame update
    private void Awake()
    {
        skeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();
    }
    void Start()
    {
        Mixer.Init();
        Mixer.SpawnSkeletonAnimation(skeletonAnimation, axieId, genesString);
        skeletonAnimation.transform.localPosition = new Vector3(0f, -0.32f, 0f);
        skeletonAnimation.transform.SetParent(transform, false);
        skeletonAnimation.transform.localScale = new Vector3(1, 1, 1);
        skeletonAnimation.timeScale = 0.5f;
        skeletonAnimation.skeleton.FindSlot("shadow").Attachment = null;
        skeletonAnimation.state.SetAnimation(0, "action/idle/normal", true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
