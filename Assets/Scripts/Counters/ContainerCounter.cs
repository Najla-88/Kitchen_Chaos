using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private GameObject Locked;


    public override void Interact(Player player)
    {

        if (Locked.activeSelf == false)
        {
            if (!player.HasKitchenObject())
            {
                // Player is not carrying anything
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
            else if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                // Player is carrying a Plate

                KitchenObject breadKitchenObject =  KitchenObject.SpawnKitchenObject(kitchenObjectSO);
                if (plateKitchenObject.TryAddIngredient(breadKitchenObject.GetKitchenObjectSO()))
                {
                    OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
                    Destroy(breadKitchenObject.gameObject);
                }
                else
                {
                    Destroy(breadKitchenObject.gameObject);
                }
            }
        }
    }
}
