using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavedDataEventsManager : MonoBehaviour
{
    [SerializeField] private PlayerMaterialDataListSO playerMaterialDataListSO;
    [SerializeField] private CounterItemsDataListSO counterItemsDataListSO;
    [SerializeField] private LevelInfoListSO levelInfoListSO;

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
        if (counterItemsDataListSO.counterItemsDataSOArray.Length > 0)
        {
            foreach (CounterItemsDataSO counterItemsDataSO in counterItemsDataListSO.counterItemsDataSOArray)
            {
                counterItemsDataSO.OnIncrease += CounterItemsDataSO_OnIncrease;
                counterItemsDataSO.OnDecrease += CounterItemsDataSO_OnDecrease;
            }
        }

        foreach (LevelInfoSO levelInfo in levelInfoListSO.levelInfoSOArray)
        {
            levelInfo.OnStarsUpdated += LevelInfo_OnStarsUpdated;
        }
    }

    private void PlayerMaterialDataSO_OnUsed(object sender, EventArgs e)
    {
        SaveManager.Instance.Save(sender.ToString());
    }

    private void PlayerMaterialDataSO_OnSold(object sender, EventArgs e)
    {
        SaveManager.Instance.Save();
    }


    private void CounterItemsDataSO_OnDecrease(object sender, EventArgs e)
    {
        SaveManager.Instance.Save();
    }

    private void CounterItemsDataSO_OnIncrease(object sender, EventArgs e)
    {
        SaveManager.Instance.Save();
    }


    private void LevelInfo_OnStarsUpdated(object sender, EventArgs e)
    {
        SaveManager.Instance.Save();
        SaveManager.Instance.Load();
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
        if (counterItemsDataListSO != null && counterItemsDataListSO.counterItemsDataSOArray != null)
        {
            foreach (CounterItemsDataSO counterItemsDataSO in counterItemsDataListSO.counterItemsDataSOArray)
            {
                counterItemsDataSO.OnIncrease -= CounterItemsDataSO_OnIncrease;
                counterItemsDataSO.OnDecrease -= CounterItemsDataSO_OnDecrease;
            }
        }
        if (levelInfoListSO != null && levelInfoListSO.levelInfoSOArray != null)
        {
            foreach (LevelInfoSO levelInfo in levelInfoListSO.levelInfoSOArray)
            {
                levelInfo.OnStarsUpdated -= LevelInfo_OnStarsUpdated;
            }
        }
    }
}
