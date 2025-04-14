using System;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    [SerializeField] private PlayerMaterialDataListSO playerMaterialDataListSO;
    [SerializeField] private CounterItemsDataListSO counterItemsDataListSO;
    [SerializeField] private LevelInfoListSO levelInfoListSO;

    private SaveData _saveData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //if (playerMaterialDataListSO.playerMaterialDataSOArray.Length > 0)
        //{
        //    foreach (PlayerMaterialDataSO playerMaterialDataSO in playerMaterialDataListSO.playerMaterialDataSOArray)
        //    {
        //        playerMaterialDataSO.OnUsed += PlayerMaterialDataSO_OnUsed;
        //        playerMaterialDataSO.OnSold += PlayerMaterialDataSO_OnSold;
        //    }
        //}
        //if (counterItemsDataListSO.counterItemsDataSOArray.Length > 0)
        //{
        //    foreach (CounterItemsDataSO counterItemsDataSO in counterItemsDataListSO.counterItemsDataSOArray)
        //    {
        //        counterItemsDataSO.OnIncrease += CounterItemsDataSO_OnIncrease;
        //        counterItemsDataSO.OnDecrease += CounterItemsDataSO_OnDecrease;
        //    }
        //}

        //foreach (LevelInfoSO levelInfo in levelInfoListSO.levelInfoSOArray)
        //{
        //    levelInfo.OnStarsUpdated += LevelInfo_OnStarsUpdated;
        //}
        Load();
    }
    //private void PlayerMaterialDataSO_OnUsed(object sender, EventArgs e)
    //{
    //    Save(sender.ToString());
    //}

    //private void PlayerMaterialDataSO_OnSold(object sender, EventArgs e)
    //{
    //    Save();
    //}


    //private void CounterItemsDataSO_OnDecrease(object sender, EventArgs e)
    //{
    //    Save();
    //}

    //private void CounterItemsDataSO_OnIncrease(object sender, EventArgs e)
    //{
    //    Save();
    //}


    //private void LevelInfo_OnStarsUpdated(object sender, EventArgs e)
    //{
    //    Save();
    //    Load();
    //}
   
    
    public void Save(string materialName = "")
    {
        _saveData = new SaveData();

        SavePlayerData(materialName);
        SaveCounterData();
        SaveLevelData();
        SaveSystem.Save(_saveData);
    }

    public void Load()
    {
        _saveData = SaveSystem.Load();

        if (_saveData == null)
        {
            InitializeDefaultValues();
            return;
        }

        LoadPlayerMaterialData();
        LoadCountersData();
        LoadLevelsData();
    }
    
    public int GetLastIndexOfCounterType(CountersManager.CounterType counterType)
    {
        foreach (var counterSaveObject in _saveData.countersSaveObjectList)
        {
            if (counterSaveObject.CounterType == counterType)
            {
                return counterSaveObject.LastIndex;
            }
        }
        return -1;
    }

    private void InitializeDefaultValues()
    {
        DataInitializer.InitializePlayerMaterialData(playerMaterialDataListSO);
        DataInitializer.InitializeCounterData(counterItemsDataListSO);
        DataInitializer.InitializeLevelsData(levelInfoListSO);

        Save();
    }

    // Save player material data
    private void SavePlayerData(string materialName)
    {
        for (int i = 0; i < playerMaterialDataListSO.playerMaterialDataSOArray.Length; i++)
        {
            if (playerMaterialDataListSO.playerMaterialDataSOArray[i].isSold)
            {
                _saveData.playerCustomSaveObject.SoldIndex.Add(i);
                if (materialName != "" && playerMaterialDataListSO.playerMaterialDataSOArray[i].ToString() == materialName)
                {
                    _saveData.playerCustomSaveObject.UsedIndex = i;
                }
            }
        }
    }

    // Save counter data
    private void SaveCounterData()
    {
        foreach (var counterData in counterItemsDataListSO.counterItemsDataSOArray)
        {
            _saveData.countersSaveObjectList.Add(new SaveData.CountersSaveObject
            {
                CounterType = counterData.counterType,
                LastIndex = counterData.lastIndex
            });
        }
    }
    
    // Save level data
    private void SaveLevelData()
    {
        foreach (var levelInfoSO in levelInfoListSO.levelInfoSOArray)
        {
            _saveData.levelsSaveData.Add(new SaveData.LevelsSaveData
            {
                levelNumber = levelInfoSO.levelNumber,
                isUnlocked = levelInfoSO.isUnlocked,
                starsCount = levelInfoSO.starsCount,
            });
        }
    }

    // Load player material data
    private void LoadPlayerMaterialData()
    {
        foreach (int soldIndex in _saveData.playerCustomSaveObject.SoldIndex)
        {
            playerMaterialDataListSO.playerMaterialDataSOArray[soldIndex].isSold = true;
        }

        for (int i = 0; i < playerMaterialDataListSO.playerMaterialDataSOArray.Length; i++)
        {
            playerMaterialDataListSO.playerMaterialDataSOArray[i].isUsed = i == _saveData.playerCustomSaveObject.UsedIndex;
        }
    }

    // Load counter data
    private void LoadCountersData()
    {
        foreach (var counterData in counterItemsDataListSO.counterItemsDataSOArray)
        {
            foreach (var counterSaveObject in _saveData.countersSaveObjectList)
            {
                if (counterData.counterType == counterSaveObject.CounterType)
                {
                    counterData.lastIndex = counterSaveObject.LastIndex;
                }
            }
        }
    }

    // Load Lavels Data
    private void LoadLevelsData()
    {
        foreach(var levelInfo in levelInfoListSO.levelInfoSOArray)
        {
            foreach(var levelSaveObject in _saveData.levelsSaveData)
            {
                if(levelInfo.levelNumber == levelSaveObject.levelNumber)
                {
                    levelInfo.isUnlocked = levelSaveObject.isUnlocked;
                    levelInfo.starsCount = levelSaveObject.starsCount;
                }
            }
        }
    }

    //private void OnDestroy()
    //{
    //    if (playerMaterialDataListSO != null && playerMaterialDataListSO.playerMaterialDataSOArray != null)
    //    {
    //        foreach (PlayerMaterialDataSO playerMaterialDataSO in playerMaterialDataListSO.playerMaterialDataSOArray)
    //        {
    //            playerMaterialDataSO.OnUsed -= PlayerMaterialDataSO_OnUsed;
    //            playerMaterialDataSO.OnSold -= PlayerMaterialDataSO_OnSold;
    //        }
    //    }
    //    if (counterItemsDataListSO != null && counterItemsDataListSO.counterItemsDataSOArray != null)
    //    {
    //        foreach (CounterItemsDataSO counterItemsDataSO in counterItemsDataListSO.counterItemsDataSOArray)
    //        {
    //            counterItemsDataSO.OnIncrease -= CounterItemsDataSO_OnIncrease;
    //            counterItemsDataSO.OnDecrease -= CounterItemsDataSO_OnDecrease;
    //        }
    //    }
    //    if (levelInfoListSO != null && levelInfoListSO.levelInfoSOArray != null)
    //    {
    //        foreach (LevelInfoSO levelInfo in levelInfoListSO.levelInfoSOArray)
    //        {
    //            levelInfo.OnStarsUpdated -= LevelInfo_OnStarsUpdated;
    //        }
    //    }
    //}
}