using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private PlayerMaterialDataListSO playerMaterialDataListSO;
    [SerializeField] private CounterItemsDataListSO counterItemsDataListSO;

    private void Awake()
    {
        SaveManager.Instance.Load();
    }

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
    }

    private void PlayerMaterialDataSO_OnSold(object sender, EventArgs e)
    {
        SaveManager.Instance.Save();
    }

    private void PlayerMaterialDataSO_OnUsed(object sender, EventArgs e)
    {
        SaveManager.Instance.Save();
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

}
