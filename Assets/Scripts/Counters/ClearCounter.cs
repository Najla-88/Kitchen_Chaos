using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO ;
    [SerializeField] private GameObject Locked;

    public override void Interact(Player player)
    {
        if (Locked.activeSelf == false)
        {
            if(!HasKitchenObject())
            {
                // There is no kitchenObject here
                if(player.HasKitchenObject())
                {
                    // Player is carrying somthing
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
                else
                {
                    // Player is not carrying anything
                }
            }
            else
            {
                // There is a kitchenObject here
                if (player.HasKitchenObject())
                {
                    // Player is carrying somthing
                    if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        // Player is holding a Plate
                        if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }else
                    {
                        // Player is not carying Plate but something else
                        if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                        {
                            // Counter is holding a Plate
                            if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                            {
                                player.GetKitchenObject().DestroySelf();
                            }
                        }
                    }
                }
                else
                {
                    // Player is not carrying anythings
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
            }
        }
    }

}
