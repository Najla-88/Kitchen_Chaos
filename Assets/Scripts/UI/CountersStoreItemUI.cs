using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountersStoreItemUI : MonoBehaviour
{
    [SerializeField] private Transform itemIconContainer;
    [SerializeField] private Transform itemTemplate;
    [SerializeField] private CounterItemsDataListSO counterItemsDataListSO;

    private void Awake()
    {
        UpdateVisual();
    }

    //private void Start()
    //{
    //    if (playerMaterialDataListSO.playerMaterialDataSOArray.Length > 0)
    //    {
    //        foreach (PlayerMaterialDataSO playerMaterialDataSO in playerMaterialDataListSO.playerMaterialDataSOArray)
    //        {
    //            playerMaterialDataSO.OnUsed += PlayerMaterialDataSO_OnUsed;
    //        }
    //    }
    //}

    //private void PlayerMaterialDataSO_OnUsed(object sender, System.EventArgs e)
    //{
    //    for (int i = 0; i < playerMaterialDataListSO.playerMaterialDataSOArray.Length; i++)
    //    {
    //        if (i != PlayerCharacterCustomized.GetCurrentMaterialIndex())
    //        {
    //            playerMaterialDataListSO.playerMaterialDataSOArray[i].UnUsePlayerMaterialDataSO();
    //        }
    //    }
    //    UpdateVisual();
    //}

    private void UpdateVisual()
    {
        itemTemplate.gameObject.SetActive(false);

        //foreach (Transform child in itemIconContainer)
        //{
        //    if (child == itemTemplate) continue;
        //    Destroy(child.gameObject);
        //}

        for (int i = 0; i < counterItemsDataListSO.counterItemsDataSOArray.Length; i++)
        {
            Transform itemTransform = Instantiate(itemTemplate, itemIconContainer);

            itemTransform.gameObject.SetActive(true);

            itemTransform.GetComponent<CountersStoreItemSingleUI>().SetItemData(counterItemsDataListSO.counterItemsDataSOArray[i]);
        }
    }

}
