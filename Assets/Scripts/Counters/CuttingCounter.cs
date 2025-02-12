using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCut;
    
    new public static void ResetStaticData()
    {
        OnAnyCut = null;    
    }

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipieSO[] cuttingRecipieSOArray;
    [SerializeField] private GameObject Locked;


    private int cuttingProress;
    public override void Interact(Player player)
    {

        if (Locked.activeSelf == false)
        {
            if (!HasKitchenObject())
            {
                // There is no kitchenObject here
                if (player.HasKitchenObject())
                {
                    // Player is carrying somthing
                    if (HasRecipieWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        // Player carrying something that can be cut
                        player.GetKitchenObject().SetKitchenObjectParent(this);
                        cuttingProress = 0;

                        CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = (float)cuttingProress / cuttingRecipieSO.cuttingProgressMax
                        });
                    }
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
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        // Player is holding a Plate
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
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

    public override void InteractAlternate(Player player)
    {

        if (Locked.activeSelf == false)
        {
            if (HasKitchenObject() && HasRecipieWithInput(GetKitchenObject().GetKitchenObjectSO()))
            {
                // There is a ktichenObject here AND it can be cut
                cuttingProress++;

                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);


                CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = (float)cuttingProress / cuttingRecipieSO.cuttingProgressMax
                });

                if (cuttingProress >= cuttingRecipieSO.cuttingProgressMax)
                {
                    KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                }

            }
        }
    }

    public bool HasRecipieWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(inputKitchenObjectSO);
        return cuttingRecipieSO != null;
    }
    public KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(inputKitchenObjectSO);
        
        if(cuttingRecipieSO != null)
        {
            return cuttingRecipieSO.output;
        }
        return null;
    }

    public CuttingRecipieSO GetCuttingRecipieSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipieSO cuttingRecipieSO in cuttingRecipieSOArray)
        {
            if (cuttingRecipieSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipieSO;
            }
        }
        return null;
    }
}
