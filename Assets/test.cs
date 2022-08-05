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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
