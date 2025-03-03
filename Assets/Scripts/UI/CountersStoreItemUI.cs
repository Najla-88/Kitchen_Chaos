using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountersStoreItemUI : MonoBehaviour
{
    [SerializeField] private Transform itemIconContainer;
    [SerializeField] private Transform itemTemplate;
    [SerializeField] private CounterItemsDataListSO counterItemsDataListSO;

    private void Start()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        itemTemplate.gameObject.SetActive(false);

        foreach (Transform child in itemIconContainer)
        {
            if (child == itemTemplate) continue;
            Destroy(child.gameObject);
        }

        for (int i = 0; i < counterItemsDataListSO.counterItemsDataSOArray.Length; i++)
        {
            Transform itemTransform = Instantiate(itemTemplate, itemIconContainer);

            itemTransform.gameObject.SetActive(true);

            itemTransform.GetComponent<CountersStoreItemSingleUI>().SetItemData(counterItemsDataListSO.counterItemsDataSOArray[i]);
        }
    }

}
