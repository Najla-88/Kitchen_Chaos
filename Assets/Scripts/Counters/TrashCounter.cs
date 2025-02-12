using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler<OnAnyObjectTrashedEventArgs> OnAnyObjectTrashed;

    public class OnAnyObjectTrashedEventArgs : EventArgs
    {
        public int coins;
    }

    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }
    public override void Interact(Player player)
    { 
        if(player.HasKitchenObject())
        {
            // Player carying somthing
            OnAnyObjectTrashed?.Invoke(this, new OnAnyObjectTrashedEventArgs {
                coins = player.GetKitchenObject().GetKitchenObjectCost()
            });
            player.GetKitchenObject().DestroySelf();
        }
    }
}
