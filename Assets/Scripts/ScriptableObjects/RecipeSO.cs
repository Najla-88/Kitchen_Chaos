using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public KitchenObjectSO plateKitchenObjectSO;

    public List<KitchenObjectSO> kitchenObjectSOList;
    public string recipeName;

    //public float availableTime;

    public int CalculateTotalCost()
    {
        int coins = 0;

        foreach (var kitchenObject in kitchenObjectSOList)
        {
            coins += kitchenObject.cost;
        }

        coins += plateKitchenObjectSO.cost;
        
        return coins;
    }
}
