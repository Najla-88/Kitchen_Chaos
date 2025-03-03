using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    [SerializeField] private PlayerMaterialDataListSO playerMaterialDataListSO;
    [SerializeField] private CounterItemsDataListSO counterItemsDataListSO;

    private const string STORE_PREFS = "StorePrefs";

    private List<CountersSaveObject> countersSaveObjectList = new List<CountersSaveObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [Serializable]
    private class PlayerCustomSaveObject
    {
        public int usedIndex;
        public List<int> soldIndex = new List<int>();
    }

    [Serializable]
    private class CountersSaveObject
    {
        public CountersManager.CounterType counterType;
        public int lastIndex;
    }

    [Serializable]
    private class SaveObject
    {
        public PlayerCustomSaveObject playerCustomSaveObject;
        public List<CountersSaveObject> countersSaveObjectList;
    }

    public void Save()
    {
        SaveObject saveObject = new SaveObject
        {
            playerCustomSaveObject = new PlayerCustomSaveObject(),
            countersSaveObjectList = new List<CountersSaveObject>(),
        };

        // Save player material data
        for (int i = 0; i < playerMaterialDataListSO.playerMaterialDataSOArray.Length; i++)
        {
            if (playerMaterialDataListSO.playerMaterialDataSOArray[i].isSold)
            {
                saveObject.playerCustomSaveObject.soldIndex.Add(i);
                if (playerMaterialDataListSO.playerMaterialDataSOArray[i].isUsed)
                {
                    saveObject.playerCustomSaveObject.usedIndex = i;
                }
            }
        }

        // Save counter data
        foreach (var counterData in counterItemsDataListSO.counterItemsDataSOArray)
        {
            saveObject.countersSaveObjectList.Add(new CountersSaveObject
            {
                counterType = counterData.counterType,
                lastIndex = counterData.lastIndex
            });
        }

        string json = JsonUtility.ToJson(saveObject);
        PlayerPrefs.SetString(STORE_PREFS, json);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(STORE_PREFS))
        {
            string json = PlayerPrefs.GetString(STORE_PREFS);
            if(json != null)
            {

                SaveObject saveObject = JsonUtility.FromJson<SaveObject>(json);

                // Load player material data
                foreach (int soldIndex in saveObject.playerCustomSaveObject.soldIndex)
                {
                    playerMaterialDataListSO.playerMaterialDataSOArray[soldIndex].isSold = true;
                }

                for(int i = 0; i< playerMaterialDataListSO.playerMaterialDataSOArray.Length; i++)
                {
                    if (i == saveObject.playerCustomSaveObject.usedIndex)
                    {
                        playerMaterialDataListSO.playerMaterialDataSOArray[i].isUsed = true;
                    }
                    else
                    {
                        playerMaterialDataListSO.playerMaterialDataSOArray[i].isUsed = false;

                    }
                }


                // Load counter data
                countersSaveObjectList = saveObject.countersSaveObjectList;
                foreach (var counterData in counterItemsDataListSO.counterItemsDataSOArray)
                {
                    foreach (var counterSaveObject in saveObject.countersSaveObjectList)
                    {
                        if (counterData.counterType == counterSaveObject.counterType)
                        {
                            counterData.lastIndex = counterSaveObject.lastIndex;
                        }
                    }
                }

            }
        }
        else
        {
            InitializeDefaultValues();
        }
    }

    private void InitializeDefaultValues()
    {
        // Initialize default values for ScriptableObjects

        // Player material data
        // make sure all material is unused and unsold
        foreach (var playerMaterialData in playerMaterialDataListSO.playerMaterialDataSOArray)
        {
            playerMaterialData.isSold = false;
            playerMaterialData.isUsed = false;
        }
        // set the first item as a default material
        if (playerMaterialDataListSO.playerMaterialDataSOArray.Length > 0)
        {
            playerMaterialDataListSO.playerMaterialDataSOArray[0].isSold = true;
            playerMaterialDataListSO.playerMaterialDataSOArray[0].isUsed = true;
        }


        // Counters
        foreach (var counterData in counterItemsDataListSO.counterItemsDataSOArray)
        {
            if (counterData.counterType == CountersManager.CounterType.Clear)
            {
                counterData.lastIndex = 1;
            }
            else if (counterData.counterType == CountersManager.CounterType.BreadContainer)
            {
                counterData.lastIndex = 1;
            }
            else if (counterData.counterType == CountersManager.CounterType.MeatContainer)
            {
                counterData.lastIndex = 1;
            }
            else if (counterData.counterType == CountersManager.CounterType.Stove)
            {
                counterData.lastIndex = 1;
            }
            else
            {
                counterData.lastIndex = 0;
            }
        }
        Save();

    }


    public int GetLastIndexOfCounterType(CountersManager.CounterType counterType)
    {
        foreach (CountersSaveObject countersSaveObject in countersSaveObjectList)
        {
            if(countersSaveObject.counterType == counterType)
            {
                return countersSaveObject.lastIndex;
            }
        }
        return -1;

    }
}