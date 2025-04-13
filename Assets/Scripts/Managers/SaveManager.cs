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

        Load();
    }

    private void Start()
    {
        foreach(LevelInfoSO levelInfo in levelInfoListSO.levelInfoSOArray)
        {
            levelInfo.OnStarsUpdated += LevelInfo_OnStarsUpdated;
        }
    }

    private void LevelInfo_OnStarsUpdated(object sender, System.EventArgs e)
    {
        Save();
        Load();
    }

    public void Save()
    {
        _saveData = new SaveData();

        SavePlayerData();
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
    private void SavePlayerData()
    {
        for (int i = 0; i < playerMaterialDataListSO.playerMaterialDataSOArray.Length; i++)
        {
            if (playerMaterialDataListSO.playerMaterialDataSOArray[i].isSold)
            {
                _saveData.playerCustomSaveObject.SoldIndex.Add(i);
                if (playerMaterialDataListSO.playerMaterialDataSOArray[i].isUsed)
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
}