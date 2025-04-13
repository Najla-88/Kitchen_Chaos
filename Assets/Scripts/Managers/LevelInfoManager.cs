using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfoManager : MonoBehaviour
{

    [SerializeField] private LevelInfoListSO levelInfoListSO;

    //private const string UNLOCKED = "unlocked-";
    public static LevelInfoManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void UnlockLevel(string levelSceneName)
    {
        //PlayerPrefs.SetInt(UNLOCKED + levelSceneName, isUnlocked);
        //PlayerPrefs.Save();
        foreach(LevelInfoSO levelInfo in levelInfoListSO.levelInfoSOArray)
        {
            if(levelInfo.scene.ToString() == levelSceneName)
            {
                levelInfo.isUnlocked = true;
            }
        }
    }
    //public int GetUnlockedPrefs(string levelSceneName)
    //{
    //    return PlayerPrefs.GetInt(UNLOCKED + levelSceneName);
    //}
}
