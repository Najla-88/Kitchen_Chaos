using UnityEngine;

public static class DataInitializer
{
    public static void InitializePlayerMaterialData(PlayerMaterialDataListSO playerMaterialDataListSO)
    {
        foreach (var playerMaterialData in playerMaterialDataListSO.playerMaterialDataSOArray)
        {
            playerMaterialData.isSold = false;
            playerMaterialData.isUsed = false;
        }

        if (playerMaterialDataListSO.playerMaterialDataSOArray.Length > 0)
        {
            playerMaterialDataListSO.playerMaterialDataSOArray[0].isSold = true;
            playerMaterialDataListSO.playerMaterialDataSOArray[0].isUsed = true;
        }
    }

    public static void InitializeCounterData(CounterItemsDataListSO counterItemsDataListSO)
    {
        foreach (var counterData in counterItemsDataListSO.counterItemsDataSOArray)
        {
            counterData.lastIndex = counterData.counterType switch
            {
                CountersManager.CounterType.Clear => 1,
                CountersManager.CounterType.BreadContainer => 1,
                CountersManager.CounterType.MeatContainer => 1,
                CountersManager.CounterType.Stove => 1,
                _ => 0
            };
        }
    }

    public static void InitializeLevelsData(LevelInfoListSO levelInfoListSO)
    {
        levelInfoListSO.levelInfoSOArray[0].isUnlocked = true;
        foreach(LevelInfoSO levelInfo in levelInfoListSO.levelInfoSOArray)
        {
            if(levelInfo.levelNumber == 1)
            {
                levelInfo.isUnlocked = true;
            }
            else
            {
                levelInfo.isUnlocked = false;
            }
            levelInfo.starsCount = -1;
        }
    }
}