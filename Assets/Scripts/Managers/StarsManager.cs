using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsManager : MonoBehaviour
{

    //private const string GAME_STARS_NUMBER = "game-stars-number-";
    private const string LEVEL_STARS_NUMBER = "level-stars-number-";

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
        int lastStarsNumber = GetLevelStarsPrefs(Loader.GetCurrentSceneName());
        if(lastStarsNumber < numberOfStars)
        {
            PlayerPrefs.SetInt(LEVEL_STARS_NUMBER + sceneName, numberOfStars);
            PlayerPrefs.Save();
        }
    }
    public int GetLevelStarsPrefs(string sceneName)
    {
        return PlayerPrefs.GetInt(LEVEL_STARS_NUMBER + sceneName, -1);
    }
}
