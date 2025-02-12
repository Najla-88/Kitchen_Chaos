using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountersStoreItemSingleUI : MonoBehaviour
{

    [SerializeField] private Button itemIconButton;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Button buyButton;
    //[SerializeField] private Button useButton;
    //[SerializeField] private Button usedButton;
    [SerializeField] private TextMeshProUGUI buyText;
    //[SerializeField] private PlayerCharacterCustomized playerCharacterCustomized;
    [SerializeField] private Button downgrateButton;

    [SerializeField] private CounterUnlockerManager counterUnlockerManager;
    private CounterItemsDataSO counterItemsDataSO;
    //private int index;

    private void Start()
    {

        //useButton.onClick.AddListener(() => {
        //    playerCharacterCustomized.ChangeMaterial(index);

        //    playerMaterialDataSO.UsePlayerMaterialDataSO();
        //});
        if(counterItemsDataSO.lastIndex == counterItemsDataSO.maxCount)
        {
            buyButton.enabled = false;
        }
        downgrateButton.onClick.AddListener(() =>
        {
            if(counterItemsDataSO.lastIndex > 0)
            {
                counterUnlockerManager.LockCounterInPrefs(counterItemsDataSO.counterType, counterItemsDataSO.lastIndex -1);
                counterItemsDataSO.UpdateLastIndex();
            }
            UpdateButtonsVisual();
        });
        buyButton.onClick.AddListener(() =>
        {
            //if (CoinsManager.Instance.DeductPlayerCoins(counterItemsDataSO.cost))
            //{
                //counterItemsDataSO.SoldPlayerMaterialDataSO();

            counterUnlockerManager.UnlockCounterInPrefs(counterItemsDataSO.counterType, counterItemsDataSO.lastIndex +1);
            counterItemsDataSO.UpdateLastIndex();

            UpdateButtonsVisual();
            //}
        });
    }

    public void SetItemData(CounterItemsDataSO counterItemsDataSO)
    {
        counterItemsDataSO.UpdateLastIndex();

        itemImage.sprite = counterItemsDataSO.sprite;
        itemName.text = counterItemsDataSO.itemName + "\n" + "(" + (counterItemsDataSO.lastIndex).ToString() + "/" + (counterItemsDataSO.maxCount).ToString() + ")";
        //this.index = index;
        buyText.text = counterItemsDataSO.cost.ToString() + "  <sprite=0>";

        this.counterItemsDataSO = counterItemsDataSO;

        //UpdateButtonsVisual();
    }

    private void UpdateButtonsVisual()
    {
        itemName.text = counterItemsDataSO.itemName + "\n" + "(" + (counterItemsDataSO.lastIndex).ToString() + "/" + (counterItemsDataSO.maxCount).ToString() + ")";

        if (counterItemsDataSO.lastIndex == counterItemsDataSO.maxCount)
        {
            buyButton.enabled = false;
        }
        else
        {
            buyButton.enabled = true;
        }

        //if (playerMaterialDataSO.isUsed)
        //{
        //    buyButton.gameObject.SetActive(false);
        //    useButton.gameObject.SetActive(false);
        //    usedButton.gameObject.SetActive(true);
        //}
        //else if (playerMaterialDataSO.isSold)
        //{
        //    buyButton.gameObject.SetActive(false);
        //    usedButton.gameObject.SetActive(false);
        //    useButton.gameObject.SetActive(true);
        //}
    }

    //public int GetItemIndex()
    //{
    //    return index;
    //}

    //public void SetButtonSelected()
    //{
    //    itemIconButton.Select();
    //}

    //public bool IsItemSold()
    //{
    //    return playerMaterialDataSO.isSold;
    //}

    //public void BuyItem()
    //{

    //    int playerCoins = CoinsManager.Instance.GetPlayerCoins();
    //    int itemCost = playerMaterialDataSO.cost;
    //    if (playerCoins >= itemCost)
    //    {
    //        CoinsManager.Instance.DeductPlayerCoins(itemCost);
    //        playerMaterialDataSO.isSold = true;
    //    }
    //}
}
