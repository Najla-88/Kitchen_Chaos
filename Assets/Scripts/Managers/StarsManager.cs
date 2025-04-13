using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsManager : MonoBehaviour
{
    //private const string LEVEL_STARS_NUMBER = "level-stars-number-";

    [SerializeField] private LevelInfoListSO levelInfoListSO;

    public event EventHandler OnGameStarsNumberChange;

    public static StarsManager Instance { get; private set; }

    private int gameStarsNumber;
    private void Awake()
    {
        Instance = this;
    }

    private readonly float[] starsLimitArray = {0.79f, 0.9f, 1f};

    private void Start()
    {
        CoinsManager.Instance.OnGameCoinsAmountChange += CoinsManager_OnGameCoinsAmountChange;
        KitchenGameManager.Instance.OnGameOver += KitchenGameManager_OnGameOver;
    }

    private void KitchenGameManager_OnGameOver(object sender, EventArgs e)
    {
        Debug.Log("gameStarsNumber " + gameStarsNumber);
        Debug.Log("gameStarsNumber " + gameStarsNumber);
        SetLevlelStarsPrefs(Loader.GetCurrentSceneName(), gameStarsNumber);
    }

    private void CoinsManager_OnGameCoinsAmountChange(object sender, EventArgs e)
    {
        float gameStatusBarAmount = CoinsManager.Instance.GetGameStatusBar();
        int numberOfStars=0;

        for (int i = 0; i < 3; i++)
        {
            numberOfStars = gameStatusBarAmount >= starsLimitArray[i] ? (numberOfStars + 1) : numberOfStars;
        }

        SetGameStars(numberOfStars);
    }

    public void SetGameStars(int numberOfStars)
    {
        gameStarsNumber = numberOfStars;
        OnGameStarsNumberChange?.Invoke(this, new EventArgs());
    }
    public int GetGameStars()
    {
        return gameStarsNumber;
    }
    public void SetLevlelStarsPrefs(string sceneName, int numberOfStars)
    {
        for(int i = 0; i< levelInfoListSO.levelInfoSOArray.Length; i++)
        {
            if(levelInfoListSO.levelInfoSOArray[i].scene.ToString() == sceneName)
            {
                levelInfoListSO.levelInfoSOArray[i].UpdateStars(numberOfStars);

                // unlock the next level
                LevelInfoSO nextLevel = levelInfoListSO.levelInfoSOArray[i + 1];
                if (numberOfStars > 0 && !nextLevel.isUnlocked )
                {
                    // if there is any condition check them before unlock
                    if (nextLevel.conditionCounterUnlockType.Length > 0)
                    {
                        bool unlockNextLevel = false;
                        for (int j = 0; j < nextLevel.conditionCounterUnlockType.Length; j++)
                        {
                            int numberOfUnlockedCounters = nextLevel.conditionCounterUnlockType[j].numberOfUnlockedCounters;
                            int lastIndexOfCounterType = SaveManager.Instance.GetLastIndexOfCounterType(nextLevel.conditionCounterUnlockType[j].counterUnlockType);

                            if (lastIndexOfCounterType >= numberOfUnlockedCounters)
                            {
                                if (j == nextLevel.conditionCounterUnlockType.Length - 1)
                                {
                                    unlockNextLevel = true;
                                }
                            }
                            else
                            {
                                unlockNextLevel = false;
                                break;
                            }
                        }
                        if (unlockNextLevel)
                        {
                            LevelInfoManager.Instance.UnlockLevel(nextLevel.scene.ToString());
                            nextLevel.isUnlocked = true;
                        }
                        else
                        {
                            nextLevel.isUnlocked = false;
                            nextLevel.starsCount = -1;
                        }
                    }
                }
                //else
                //{
                //    LevelInfoManager.Instance.SetUnlockedPrefs(nextLevel.scene.ToString(), 1);
                //    nextLevel.isUnlocked = true;
                //}
                break;

            }
        }
        //int lastStarsNumber = GetLevelStarsPrefs(Loader.GetCurrentSceneName());
        //if(lastStarsNumber < numberOfStars)
        //{
        //    PlayerPrefs.SetInt(LEVEL_STARS_NUMBER + sceneName, numberOfStars);
        //    PlayerPrefs.Save();
        //}
    }
    //public int GetLevelStarsPrefs(string sceneName)
    //{
    //    return PlayerPrefs.GetInt(LEVEL_STARS_NUMBER + sceneName, -1);
    //}
}
