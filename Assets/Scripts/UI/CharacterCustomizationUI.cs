using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomizationUI : MonoBehaviour
{
    [SerializeField] private Transform itemIconContainer;
    [SerializeField] private Transform itemTemplate;
    [SerializeField] private PlayerMaterialDataListSO playerMaterialDataListSO;

    private void Start()
    {
        if (playerMaterialDataListSO.playerMaterialDataSOArray.Length > 0)
        {
            foreach (PlayerMaterialDataSO playerMaterialDataSO in playerMaterialDataListSO.playerMaterialDataSOArray)
            {
                playerMaterialDataSO.OnUsed += PlayerMaterialDataSO_OnUsed;
                playerMaterialDataSO.OnSold += PlayerMaterialDataSO_OnSold;
            }
        }
        UpdateVisual();
    }

    private void PlayerMaterialDataSO_OnSold(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void PlayerMaterialDataSO_OnUsed(object sender, System.EventArgs e)
    {
        foreach (PlayerMaterialDataSO playerMaterialDataSO in playerMaterialDataListSO.playerMaterialDataSOArray)
        {
            if((object)sender != playerMaterialDataSO)
            {
                playerMaterialDataSO.isUsed = false;
            }
        }
        UpdateVisual();
    }

    private void OnDestroy()
    {
        if (playerMaterialDataListSO != null && playerMaterialDataListSO.playerMaterialDataSOArray != null)
        {
            foreach (PlayerMaterialDataSO playerMaterialDataSO in playerMaterialDataListSO.playerMaterialDataSOArray)
            {
                playerMaterialDataSO.OnUsed -= PlayerMaterialDataSO_OnUsed;
                playerMaterialDataSO.OnSold -= PlayerMaterialDataSO_OnSold;
            }
        }
    }
    private void UpdateVisual()
    {

        itemTemplate.gameObject.SetActive(false);

        foreach (Transform child in itemIconContainer)
        {
            if (child == itemTemplate) continue;
            Destroy(child.gameObject);
        }

        for (int i = 0; i < playerMaterialDataListSO.playerMaterialDataSOArray.Length; i++)
        {
            Transform itemTransform = Instantiate(itemTemplate, itemIconContainer);

            itemTransform.gameObject.SetActive(true);

            itemTransform.GetComponent<StoreItemSingleUI>().SetItemData(playerMaterialDataListSO.playerMaterialDataSOArray[i], i);
        }
    }


}
