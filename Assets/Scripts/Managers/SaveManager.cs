using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // we need these to update them
    [SerializeField] private CounterItemsDataListSO counterItemsDataListSO;
    [SerializeField] private PlayerMaterialDataListSO playerMaterialDataListSO;

    private void Awake()
    {
        Load();
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
        Save();
    }

    private void PlayerMaterialDataSO_OnUsed(object sender, EventArgs e)
    {
        Save();
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

    private const string STORE_PREFS = "StorePrefs";

    [Serializable]
    public class CountersSaveObject
    {
        public CountersManager.CounterType counterType;
        public int lastIndex;
    }

    [Serializable]
    public class PlayerCustomSaveObject
    {
        public int usedIndex;
        public List<int> soldIndex;

        // Constructor to initialize the soldIndex list
        public PlayerCustomSaveObject()
        {
            soldIndex = new List<int>();
        }
    }

    public class SaveObject
    {
        public List<CountersSaveObject> countersSaveObjectList;
        public PlayerCustomSaveObject playerCustomSaveObject;
    }

    public void Save()
    {
        List<CountersSaveObject> countersSaveObjectList = new List<CountersSaveObject>();


        CounterItemsDataSO[] counterItemsDataSOArrray = counterItemsDataListSO.counterItemsDataSOArray;

        for(int i = 0; i < counterItemsDataSOArrray.Length; i++)
        {
            CountersSaveObject countersSaveObject = new CountersSaveObject();
            countersSaveObject.counterType = counterItemsDataSOArrray[i].counterType;
            countersSaveObject.lastIndex = counterItemsDataSOArrray[i].lastIndex;

            countersSaveObjectList.Add(countersSaveObject);
        }

        PlayerCustomSaveObject playerCustomSaveObject = new PlayerCustomSaveObject();
        
        PlayerMaterialDataSO[] playerMaterialDataListSOArray = playerMaterialDataListSO.playerMaterialDataSOArray;

        for (int i =0; i< playerMaterialDataListSOArray.Length; i++)
        {
            if (playerMaterialDataListSOArray[i].isSold)
            {
                playerCustomSaveObject.soldIndex.Add(i);
                if (playerMaterialDataListSOArray[i].isUsed)
                {
                    playerCustomSaveObject.usedIndex = i;
                }
            }
        }

        SaveObject saveObject = new SaveObject
        {
            countersSaveObjectList = countersSaveObjectList,
            playerCustomSaveObject = playerCustomSaveObject,
        };

        string json = JsonUtility.ToJson(saveObject);

        PlayerPrefs.SetString(STORE_PREFS, json);
    }

    public void Load()
    {
        string json = PlayerPrefs.GetString(STORE_PREFS);
        if (string.IsNullOrEmpty(json))
        {
            InitializeDefaultValues();
        }
        else
        {
            // Load saved data
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(json);
            if (saveObject != null)
            {
                LoadPlayerCustom(saveObject);
            }
        }
    }

    private void LoadPlayerCustom(SaveObject saveObject)
    {
        PlayerCustomSaveObject playerCustomSaveObject = saveObject.playerCustomSaveObject;

        PlayerMaterialDataSO[] playerMaterialDataListSOArray = playerMaterialDataListSO.playerMaterialDataSOArray;
        foreach (int soldIndex in playerCustomSaveObject.soldIndex)
        {
            playerMaterialDataListSOArray[soldIndex].isSold = true;
        }
        playerMaterialDataListSOArray[playerCustomSaveObject.usedIndex].isUsed = true;
    }
    private void LoadCounters(SaveObject saveObject)
    {
        List<CountersSaveObject> countersSaveObjectList = saveObject.countersSaveObjectList;

        CounterItemsDataSO[] counterItemsDataSOArrray = counterItemsDataListSO.counterItemsDataSOArray;
        foreach (CountersSaveObject countersSaveObject in countersSaveObjectList)
        {
            foreach(CounterItemsDataSO counterItemsDataSO in counterItemsDataSOArrray)
            {
                if(counterItemsDataSO.counterType == countersSaveObject.counterType)
                {
                    counterItemsDataSO.lastIndex = countersSaveObject.lastIndex;
                }
            }
        }
    }
    private void InitializeDefaultValues()
    {
        // Set default values for playerMaterialDataListSO
        PlayerMaterialDataSO[] playerMaterialDataListSOArray = playerMaterialDataListSO.playerMaterialDataSOArray;
        for (int i = 0; i < playerMaterialDataListSOArray.Length; i++)
        {
            playerMaterialDataListSOArray[i].isSold = false;
            playerMaterialDataListSOArray[i].isUsed = false;
        }

        if (playerMaterialDataListSOArray.Length > 0)
        {
            playerMaterialDataListSOArray[0].isSold = true;
            playerMaterialDataListSOArray[0].isUsed = true;
        }

        CounterItemsDataSO[] counterItemsDataListSOArray = counterItemsDataListSO.counterItemsDataSOArray;

        for(int i = 0; i < counterItemsDataListSOArray.Length; i++)
        {
            Debug.Log("hi");
            Debug.Log(counterItemsDataListSOArray[0].counterType.ToString());
            if (counterItemsDataListSOArray[0].counterType.ToString() == "Clear")
            {
                counterItemsDataListSOArray[0].lastIndex = 1;
            }
            else if (counterItemsDataListSOArray[0].counterType.ToString() == "Bread")
            {
                counterItemsDataListSOArray[0].lastIndex = 1;
            }
            else if (counterItemsDataListSOArray[0].counterType.ToString() == "Meat")
            {
                counterItemsDataListSOArray[0].lastIndex = 1;
            }
            else if (counterItemsDataListSOArray[0].counterType.ToString() == "Stove")
            {
                counterItemsDataListSOArray[0].lastIndex = 1;
            }
        }

        Save();
    }
}


