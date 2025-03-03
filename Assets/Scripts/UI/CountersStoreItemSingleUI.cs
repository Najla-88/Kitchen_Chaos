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
    [SerializeField] private TextMeshProUGUI buyText;
    [SerializeField] private Button downgrateButton;

    private CounterItemsDataSO counterItemsDataSO;

    private void Start()
    {
        // disable button if reatch the max
        if(counterItemsDataSO.lastIndex == counterItemsDataSO.maxCount)
        {
            buyButton.enabled = false;
        }
        
        buyButton.onClick.AddListener(() =>
        {
            //if (CoinsManager.Instance.DeductPlayerCoins(counterItemsDataSO.cost))
            //{
                counterItemsDataSO.IncreaseLastIndex();

                UpdateButtonsVisual();
            //}
        });
    }

    public void SetItemData(CounterItemsDataSO counterItemsDataSO)
    {
        itemImage.sprite = counterItemsDataSO.sprite;
        itemName.text = counterItemsDataSO.itemName + "\n" + "(" + (counterItemsDataSO.lastIndex).ToString() + "/" + (counterItemsDataSO.maxCount).ToString() + ")";
        buyText.text = counterItemsDataSO.cost.ToString() + "  <sprite=0>";
        this.counterItemsDataSO = counterItemsDataSO;
    }

    private void UpdateButtonsVisual()
    {
        int lastIndex = (counterItemsDataSO.lastIndex);
        int maxCount = (counterItemsDataSO.maxCount);

        itemName.text = counterItemsDataSO.itemName + "\n" + "(" + lastIndex.ToString() + "/" + maxCount.ToString() + ")";

        if (lastIndex == maxCount)
        {
            buyButton.enabled = false;
        }
        else
        {
            buyButton.enabled = true;
        }
    }
}
