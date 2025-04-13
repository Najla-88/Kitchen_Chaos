using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public PlayerCustomSaveObject playerCustomSaveObject = new PlayerCustomSaveObject();
    public List<CountersSaveObject> countersSaveObjectList = new List<CountersSaveObject>();
    public List<LevelsSaveData> levelsSaveData = new List<LevelsSaveData>();

    [Serializable]
    public class PlayerCustomSaveObject
    {
        public int UsedIndex;
        public List<int> SoldIndex = new List<int>();
    }

    [Serializable]
    public class CountersSaveObject
    {
        public CountersManager.CounterType CounterType;
        public int LastIndex;
    }

    [Serializable]
    public class LevelsSaveData 
    {
        public int levelNumber;
        public bool isUnlocked;
        public int starsCount;
    }

}