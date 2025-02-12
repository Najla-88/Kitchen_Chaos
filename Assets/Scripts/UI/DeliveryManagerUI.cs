using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerUI : MonoBehaviour
{

    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;
    [SerializeField] private TextMeshProUGUI ordersNumberText;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeUpdated += DeliveryManager_OnRecipeUpdated;
        DeliveryManager.Instance.OnSpawnedRecipeMaxUpdated += DeliveryManager_OnSpawnedRecipeMaxUpdated;

        UpdateVisual();
    }

    private void DeliveryManager_OnSpawnedRecipeMaxUpdated(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeUpdated(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (WaitingRecipe waitingRecipe in DeliveryManager.Instance.GetWaitingRecipeList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(waitingRecipe);
        }

        ordersNumberText.text = DeliveryManager.Instance.GetSpawnedRecipeMax().ToString() + " <sprite=1>";
    }

}
