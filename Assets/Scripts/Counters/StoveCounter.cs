using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs: EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    [SerializeField] private FryingRecipieSO[] fryingRecipieSOArray;
    [SerializeField] private BurningRecipieSO[] burningRecipieSOArray;
    [SerializeField] private GameObject Locked;


    private State state;
    private float fryingTimer;
    private FryingRecipieSO fryingRecipieSO;
    private float burningTimer;
    private BurningRecipieSO burningRecipieSO;

    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipieSO.fryingTimerMax
                    });

                    if (fryingTimer > fryingRecipieSO.fryingTimerMax)
                    {
                        // Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(fryingRecipieSO.output, this);

                        burningRecipieSO = GetBurningRecipieSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        state = State.Fried;
                        burningTimer = 0f;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                    }
                    break;
                case State.Fried:

                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipieSO.burningTimerMax
                    });

                    if (burningTimer > burningRecipieSO.burningTimerMax)
                    {
                        // Burned
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipieSO.output, this);

                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });

                    }

                    break;
                case State.Burned:
                    break;
            }
        }
    }
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
                        // Player carrying something that can be Fried
                        player.GetKitchenObject().SetKitchenObjectParent(this);

                        fryingRecipieSO = GetFryingRecipieSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        state = State.Frying;
                        fryingTimer = 0f;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = fryingTimer / fryingRecipieSO.fryingTimerMax
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

                            state = State.Idle;
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                            {
                                state = state
                            });

                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                            {
                                progressNormalized = 0f
                            });
                        }
                    }
                }
                else
                {
                    // Player is not carrying anythings
                    GetKitchenObject().SetKitchenObjectParent(player);

                    state = State.Idle;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });

                }
            }
        }
    }


    public bool HasRecipieWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipieSO fryingRecipieSO = GetFryingRecipieSOWithInput(inputKitchenObjectSO);
        return fryingRecipieSO != null;
    }
    public KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipieSO fryingRecipieSO = GetFryingRecipieSOWithInput(inputKitchenObjectSO);

        if (fryingRecipieSO != null)
        {
            return fryingRecipieSO.output;
        }
        return null;
    }

    public FryingRecipieSO GetFryingRecipieSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipieSO fryingRecipieSO in fryingRecipieSOArray)
        {
            if (fryingRecipieSO.input == inputKitchenObjectSO)
            {
                return fryingRecipieSO;
            }
        }
        return null;
    }
    public BurningRecipieSO GetBurningRecipieSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipieSO burningRecipieSO in burningRecipieSOArray)
        {
            if (burningRecipieSO.input == inputKitchenObjectSO)
            {
                return burningRecipieSO;
            }
        }
        return null;
    }

    public bool IsFried()
    {
        return state == State.Fried;
    }
}