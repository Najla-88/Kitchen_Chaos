using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private Transform step1;
    //[SerializeField] private Transform step2;
    //[SerializeField] private Transform step3;
    //[SerializeField] private Transform step4;
    //[SerializeField] private Transform step5;
    //[SerializeField] private Transform step6;
    //[SerializeField] private Transform step7;
    //[SerializeField] private Transform step8;
    //[SerializeField] private Transform step9;
    //[SerializeField] private Transform step10;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        step1.gameObject.SetActive(true);
        KitchenGameManager.Instance.TogglePauseGame();
    }
}
