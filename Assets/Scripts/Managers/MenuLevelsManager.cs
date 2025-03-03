using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLevelsManager : MonoBehaviour
{
    [SerializeField] private LevelInfoListSO levelInfoListSO;
    //[SerializeField] private CounterUnlockerManager counterUnlockerManager;

    public static event EventHandler OnLevelsInfoUpdated;

    public static void ResetStaticData()
    {
        OnLevelsInfoUpdated = null;
    }
    private void Start()
    {

        SaveManager.Instance.Load();
        //LevelInfoManager.Instance.SetUnlockedPrefs(levelInfoListSO.levelInfoSOArray[0].scene.ToString(), 0);
        //StarsManager.Instance.SetLevlelStarsPrefs(levelInfoListSO.levelInfoSOArray[0].scene.ToString(), -1);
        //LevelInfoManager.Instance.SetUnlockedPrefs(levelInfoListSO.levelInfoSOArray[1].scene.ToString(), 0);
        //StarsManager.Instance.SetLevlelStarsPrefs(levelInfoListSO.levelInfoSOArray[1].scene.ToString(), -1);
        //LevelInfoManager.Instance.SetUnlockedPrefs(levelInfoListSO.levelInfoSOArray[2].scene.ToString(), 0);
        //StarsManager.Instance.SetLevlelStarsPrefs(levelInfoListSO.levelInfoSOArray[2].scene.ToString(), -1);

        //Debug.Log(" unlocked : " + LevelInfoManager.Instance.GetUnlockedPrefs(levelInfoListSO.levelInfoSOArray[0].scene.ToString()));
        //Debug.Log(" stars : " + StarsManager.Instance.GetLevelStarsPrefs(levelInfoListSO.levelInfoSOArray[0].scene.ToString()));
        //Debug.Log(" unlocked : " + LevelInfoManager.Instance.GetUnlockedPrefs(levelInfoListSO.levelInfoSOArray[1].scene.ToString()));
        //Debug.Log(" stars : " + StarsManager.Instance.GetLevelStarsPrefs(levelInfoListSO.levelInfoSOArray[1].scene.ToString()));

        UpdateLevelsInfoSO();
    }

    private void UpdateLevelsInfoSO()
    {
        // loop in Levels
        for (int i = 0; i < levelInfoListSO.levelInfoSOArray.Length; i++)
        {
            LevelInfoSO currentLevel = levelInfoListSO.levelInfoSOArray[i];

            // is current level unlocked?
            if (LevelInfoManager.Instance.GetUnlockedPrefs(currentLevel.scene.ToString()) == 1)
            {
                currentLevel.levelUnlocked = true;
                currentLevel.starsCount = StarsManager.Instance.GetLevelStarsPrefs(currentLevel.scene.ToString());
            }
            // if the current level locked
            // check if there a previos level then check if the previos level starsCount >=0
            else if (i == 0 || levelInfoListSO.levelInfoSOArray[i - 1].starsCount >= 0)
            {
                // if there conditions check them before unlock
                if (currentLevel.conditionCounterUnlockType.Length > 0)
                {
                    for (int j = 0; j < currentLevel.conditionCounterUnlockType.Length; j++)
                    {
                        int numberOfUnlockedCounters = currentLevel.conditionCounterUnlockType[j].numberOfUnlockedCounters;
                        int lastIndexOfCounterType = SaveManager.Instance.GetLastIndexOfCounterType(currentLevel.conditionCounterUnlockType[j].counterUnlockType);

                        //Debug.Log("numberOfUnlockedCounters = "+ numberOfUnlockedCounters);
                        //Debug.Log("lastIndexOfCounterType = " + lastIndexOfCounterType);
                        //Debug.Log("counterUnlockType = " + currentLevel.conditionCounterUnlockType[j].counterUnlockType);
                        if (lastIndexOfCounterType >= numberOfUnlockedCounters)
                        {
                            if (j == currentLevel.conditionCounterUnlockType.Length - 1)
                            {
                                LevelInfoManager.Instance.SetUnlockedPrefs(currentLevel.scene.ToString(), 1);
                                currentLevel.levelUnlocked = true;
                            }
                        }
                        else
                        {
                            currentLevel.levelUnlocked = false;
                            currentLevel.starsCount = -1;
                            break;
                        }
                    }
                }
                // if there is no any condition unlock
                else
                {
                    LevelInfoManager.Instance.SetUnlockedPrefs(currentLevel.scene.ToString(), 1);
                    currentLevel.levelUnlocked = true;
                }
            }
            // if the previos level didnt played yet
            else
            {
                currentLevel.levelUnlocked = false;
                currentLevel.starsCount = -1;
            }
        }
        OnLevelsInfoUpdated?.Invoke(this, new EventArgs());
    }
}



