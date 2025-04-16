using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelInfoSO : ScriptableObject
{
    public int levelNumber;
    public Loader.Scene scene;
    public bool isUnlocked;
    public int starsCount=-1;

    public RecipeListSO recipeListSOInLevel;
    public int spawnedRecipeMaxInLevel;

    // calculate them
    //public int coinsInLevel;
    //public float recipeTimerInLevel;

    public ConditionCounterUnlockType[] conditionCounterUnlockType;

    public event EventHandler OnStarsUpdated;

    public void UpdateStars(int starsCount)
    {
        this.starsCount = starsCount;
        OnStarsUpdated?.Invoke(this, new EventArgs());
    }

    public int GetLessExpensiveRecipie()
    {
        int lessExpensiveRecipie = recipeListSOInLevel.recipeSOList[0].CalculateTotalCost();
        for (int i=0; i< recipeListSOInLevel.recipeSOList.Count; i++)
        {
            int recipieCost = recipeListSOInLevel.recipeSOList[i].CalculateTotalCost();
            lessExpensiveRecipie = lessExpensiveRecipie > recipieCost ? recipieCost : lessExpensiveRecipie;
        }
        return lessExpensiveRecipie;
    }

    public int GetSpawnedRecipeMaxInLevel()
    {
        return spawnedRecipeMaxInLevel;
    }
}

[Serializable]
public class ConditionCounterUnlockType {
    public CountersManager.CounterType counterUnlockType;
    public int numberOfUnlockedCounters;
}
