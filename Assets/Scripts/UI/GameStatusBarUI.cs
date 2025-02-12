using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatusBarUI : MonoBehaviour
{
    [SerializeField] private Image statusBarImage;


    private void Start()
    {
        CoinsManager.Instance.OnGameCoinsAmountChange += CoinsManager_OnLevelCoinsAmountChange;
        
        statusBarImage.fillAmount = CoinsManager.Instance.GetGameStatusBar();
    }

    private void CoinsManager_OnLevelCoinsAmountChange(object sender, System.EventArgs e)
    {
        statusBarImage.fillAmount = CoinsManager.Instance.GetGameStatusBar();
    }

}
