using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject chooseCardUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State == GameState.ChooseCard || GameManager.Instance.State == GameState.Idle)
        {
            chooseCardUI.SetActive(true);
        }
        else
        {
            chooseCardUI.SetActive(false);
        }
    }
}
