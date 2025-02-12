using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfoManager : MonoBehaviour
{

    private const string UNLOCKED = "unlocked-";
    public static LevelInfoManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SetUnlockedPrefs(string levelSceneName, int isUnlocked)
    {
        PlayerPrefs.SetInt(UNLOCKED + levelSceneName, isUnlocked);
        PlayerPrefs.Save();
    }
    public int GetUnlockedPrefs(string levelSceneName)
    {
        return PlayerPrefs.GetInt(UNLOCKED + levelSceneName);
    }
}
