using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemSingleUI : MonoBehaviour
{
    [SerializeField] private Button itemIconButton;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button useButton;
    [SerializeField] private Button usedButton;
    [SerializeField] private TextMeshProUGUI buyText;
    [SerializeField] private PlayerCharacterCustomized playerCharacterCustomized;

    private PlayerMaterialDataSO playerMaterialDataSO;
    private int index;

    private void Start()
    {
        useButton.onClick.AddListener(() => {
            playerCharacterCustomized.ChangeMaterial(index);

            playerMaterialDataSO.UsePlayerMaterialDataSO();
        });

        buyButton.onClick.AddListener(() =>
        {
            //if (CoinsManager.Instance.DeductPlayerCoins(playerMaterialDataSO.cost))
            //{
                playerMaterialDataSO.SoldPlayerMaterialDataSO();
                UpdateButtonsVisual();
            //}
        });
    }

    public void SetItemData(PlayerMaterialDataSO playerMaterialDataSO, int index)
    {
        itemIconButton.GetComponent<Image>().color = playerMaterialDataSO.material.color;
        itemName.text = playerMaterialDataSO.material.name;
        this.index = index;
        buyText.text = "BUY " + playerMaterialDataSO.cost.ToString() + "  <sprite=0>";

        this.playerMaterialDataSO = playerMaterialDataSO;
        
        UpdateButtonsVisual();
    }

    private void UpdateButtonsVisual()
    {
        if (playerMaterialDataSO.isUsed)
        {
            buyButton.gameObject.SetActive(false);
            useButton.gameObject.SetActive(false);
            usedButton.gameObject.SetActive(true);
        }
        else if (playerMaterialDataSO.isSold)
        {
            buyButton.gameObject.SetActive(false);
            usedButton.gameObject.SetActive(false);
            useButton.gameObject.SetActive(true);
        }
    }

    public int GetItemIndex()
    {
        return index;
    }

    public void SetButtonSelected()
    {
        itemIconButton.Select();
    }

    public bool IsItemSold()
    {
        return playerMaterialDataSO.isSold;
    }

    public void BuyItem()
    {

        int playerCoins = CoinsManager.Instance.GetPlayerCoins();
        int itemCost = playerMaterialDataSO.cost;
        if (playerCoins >= itemCost)
        {
            CoinsManager.Instance.DeductPlayerCoins(itemCost);
            playerMaterialDataSO.isSold = true;
        }    
    }
    //// Add a method to set the index
    //public void SetIndex(int index)
    //{
    //    indexText.text = index.ToString();
    //}
}