using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelInfoSO : ScriptableObject
{
    //public int sadRecipeTimeInLevel;
    //public int angryRecipeTimeInLevel;

    public int spawnedRecipeMaxInLevel;
    public int coinsInLevel;
    public float recipeTimerInLevel;
    public RecipeListSO recipeListSOInLevel;

    public int levelNumber;
    public int starsCount=-1;
    public bool isUnlocked;
    public Loader.Scene scene;

    public ConditionCounterUnlockType[] conditionCounterUnlockType;

    public event EventHandler OnStarsUpdated;

    public void UpdateStars(int starsCount)
    {
        this.starsCount = starsCount;
        OnStarsUpdated?.Invoke(this, new EventArgs());
    }
}

[Serializable]
public class ConditionCounterUnlockType {
    public CountersManager.CounterType counterUnlockType;
    public int numberOfUnlockedCounters;
}
