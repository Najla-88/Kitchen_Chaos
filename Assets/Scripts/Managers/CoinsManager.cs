using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour 
{
    private const string PLAYER_PREFS_COINS = "PlayerPrefsCoins";

    public static CoinsManager Instance { get; private set; }

    public event EventHandler OnGameCoinsAmountChange;
    public event EventHandler OnPlayerCoinsAmountChange;

    private int playerCoins;

    private int coinsInGame=0;


    private void Awake()
    {
        Instance = this;

        playerCoins = PlayerPrefs.GetInt(PLAYER_PREFS_COINS, 0);

    }

    private void Start()
    {
        if (Loader.IsNotLevelScene())
        {
            DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;

            KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

            TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
        }
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, TrashCounter.OnAnyObjectTrashedEventArgs e)
    {
        DeductCoinsInLevel(e.coins);
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
        { 
            AddPlayerCoins(coinsInGame);
        }
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, DeliveryManager.OnRecipeSuccessChangedEventArgs e)
    {
        AddCoinsInLevel(e.coins);
    }

    public void AddCoinsInLevel(int coins)
    {
        coinsInGame += coins;

        OnGameCoinsAmountChange?.Invoke(this, EventArgs.Empty);
    }
    public bool DeductCoinsInLevel(int amount)
    {
        if (coinsInGame >= amount)
        {
            coinsInGame -= amount;

            PlayerPrefs.SetInt(PLAYER_PREFS_COINS, coinsInGame);
            PlayerPrefs.Save();

            OnGameCoinsAmountChange?.Invoke(this, EventArgs.Empty);
            return true;
        }
        return false;
    }

    public int GetCoinsInLevel()
    {
        return coinsInGame;
    }
    public void AddPlayerCoins(int coins)
    {
        playerCoins += coins;

        PlayerPrefs.SetInt(PLAYER_PREFS_COINS, playerCoins);
        PlayerPrefs.Save();

        OnPlayerCoinsAmountChange?.Invoke(this, EventArgs.Empty);
    }
    public bool DeductPlayerCoins(int amount)
    {
        if (playerCoins >= amount)
        {
            playerCoins -= amount;

            PlayerPrefs.SetInt(PLAYER_PREFS_COINS, playerCoins);
            PlayerPrefs.Save();

            OnPlayerCoinsAmountChange?.Invoke(this, EventArgs.Empty);   
            return true;
        }
        return false;
    }

    public int GetPlayerCoins()
    {
        return playerCoins;
    }

    public float GetGameStatusBar()
    {
        int goalCoins = DeliveryManager.Instance.GetGoalCoins();
        if (goalCoins != 0)
        {
            return (float)coinsInGame / goalCoins;
        }
        return 0;
    }
}
