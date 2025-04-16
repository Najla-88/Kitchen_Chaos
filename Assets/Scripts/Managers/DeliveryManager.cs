using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler <OnRecipeSuccessChangedEventArgs> OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeUpdated;
    public event EventHandler OnSpawnedRecipeMaxUpdated;
    public class OnRecipeSuccessChangedEventArgs : EventArgs
    {
        public int coins;
    }

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private LevelInfoSO levelInfoSO;


    // Get From LevelInfoSO
    private RecipeListSO recipeListSO;
    private int spawnedRecipeMax;

    // Dont need edit
    private List<WaitingRecipe> waitingRecipesList;
    
    private int sadRecipeTime = 7;
    private int angryRecipeTime = 4;

    private int successfulRecipesAmount;
    private int lostRecipesAmount;
    
    // for countdown
    private int waitingRecipesMax = 3;
    private float spawnRecipeTimerMax = 10f;
    private float spawnRecipeTimer;
    private float recipeTimer = 20;



    private void Awake()
    {
        Instance = this;

        waitingRecipesList = new List<WaitingRecipe>();
    }

    private void Start()
    {
        recipeListSO = levelInfoSO.recipeListSOInLevel;
        spawnedRecipeMax = levelInfoSO.GetSpawnedRecipeMaxInLevel();
    }

    private void Update()
    {
        for (int i = 0; i < waitingRecipesList.Count; i++)
        {
            waitingRecipesList[i].timer -= Time.deltaTime;

            if (waitingRecipesList[i].timer <= 0)
            {
                OnRecipeUpdated?.Invoke(this, EventArgs.Empty);
                waitingRecipesList.RemoveAt(i);
                lostRecipesAmount++;
                i--;
            }
            else if (waitingRecipesList[i].timer <= sadRecipeTime)
            {
                OnRecipeUpdated?.Invoke(this, EventArgs.Empty);
            }
            else if (waitingRecipesList[i].timer <= angryRecipeTime)
            {
                OnRecipeUpdated?.Invoke(this, EventArgs.Empty);
            }
        }
        
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f && spawnedRecipeMax > 0)
            //if (spawnRecipeTimer <= 0f)
        {
            if (KitchenGameManager.Instance.IsGamePlaying() && waitingRecipesList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipesList.Add(new WaitingRecipe(waitingRecipeSO, recipeTimer));

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);

                spawnedRecipeMax--;
                OnSpawnedRecipeMaxUpdated?.Invoke(this, EventArgs.Empty);

                spawnRecipeTimer = spawnRecipeTimerMax;

            }
        }
    }

    public void DeleverRecipe(PlateKitchenObject plateKitchenObject)
    {
        int successfulRecipeCoint = 0;

        for(int i=0; i < waitingRecipesList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipesList[i].recipeSO;

            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // Has the same number of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    // Cycling through all ingredients in recipe
                    bool ingredientFound = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        // Cycling through all ingredients in Plate
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            // Ingredient matches!
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        // This Recipe Ingredient was not found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }
                if (plateContentsMatchesRecipe)
                {
                    // Player delevered the correct recipies
                    successfulRecipeCoint = waitingRecipesList[i].recipeSO.CalculateTotalCost();

                    if (waitingRecipesList[i].timer <= angryRecipeTime)
                    {
                        successfulRecipeCoint -= waitingRecipesList[i].recipeSO.CalculateTotalCost() / 2;
                    }
                    else if (waitingRecipesList[i].timer <= sadRecipeTime)
                    {
                        successfulRecipeCoint -= waitingRecipesList[i].recipeSO.CalculateTotalCost() / 3;
                    }

                    successfulRecipesAmount++;

                    waitingRecipesList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, new OnRecipeSuccessChangedEventArgs{
                        coins = successfulRecipeCoint
                    });;

                    return;
                }
            }
        }

        // No matches found!
        // Player did not deliver a correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);

    }

    public List<WaitingRecipe> GetWaitingRecipeList()
    {
        return waitingRecipesList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
    public int GetLostRecipesAmount()
    {
        return lostRecipesAmount;
    }

    public bool IsGameOver()
    {
        //return spawnedRecipeMax == 0 && waitingRecipesList.Count == 0;

        //return successfulRecipesAmount == spawnedRecipeMaxInLevel;
        //bool goalComplete = CoinsManager.Instance.GetGameStatusBar() >= 1;

        bool emptyWaitingRecipes = spawnedRecipeMax + waitingRecipesList.Count <= 0;

        //return goalComplete || emptyWaitingRecipes;
        return emptyWaitingRecipes;
    }

    public int GetGoalCoins()
    {
        int lessRecipe = levelInfoSO.GetLessExpensiveRecipie();
        int goalCoins = levelInfoSO.GetSpawnedRecipeMaxInLevel() * lessRecipe;
        goalCoins -= lessRecipe / 2;
        return goalCoins;
    }

    public int GetSpawnedRecipeMax()
    {
        return spawnedRecipeMax;
    }
}
