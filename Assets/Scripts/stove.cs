using System;
using UnityEngine;

public class Stove : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgessChangedEventArgs> OnProgressChanged;
    [SerializeField] private FryingObjectCO[] fryingArray;
    [SerializeField] private BurnObjectCO[] burnArray;
    public static event EventHandler<OnstateChangedEventArgs> OnStateChange;

    public class OnstateChangedEventArgs : EventArgs
    {
        public State state;
    }
    
    private float fryTime;
    private float burnTime;
    private FryingObjectCO fryingObjectCo;
    private BurnObjectCO burnObjectCo;

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burnt,
    }
    private State state;
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
                    fryTime += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgessChangedEventArgs
                    {
                        progressNormalized = fryTime/fryingObjectCo.FryingMax
                    });
                    if (fryTime > fryingObjectCo.FryingMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchObject(fryingObjectCo.output, this);
                        state = State.Fried;
                        burnObjectCo =  getburnObjectCO(GetKitchenObject().GetKitcheenObjectCO());
                        burnTime = 0f;
                        OnStateChange?.Invoke(this, new OnstateChangedEventArgs{ state = this.state});
                    }
                    
                    break;
                case State.Fried:
                    burnTime += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgessChangedEventArgs
                    {
                        progressNormalized = burnTime/burnObjectCo.burnMax
                    });
                    if (burnTime > burnObjectCo.burnMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchObject(burnObjectCo.output, this);
                        state = State.Burnt;
                        OnStateChange?.Invoke(this, new OnstateChangedEventArgs{ state = this.state});
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgessChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burnt:
                    break;
                
            }   
        }
        Debug.Log(state);
       
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (hasRecipe(player.GetKitchenObject().GetKitcheenObjectCO()))
                {
                    player.GetKitchenObject().SetkitchenObjectParent(this);
                    fryingObjectCo = getFryingObjectCO(GetKitchenObject().GetKitcheenObjectCO());
                    state = State.Frying;
                    fryTime = 0f;
                    OnStateChange?.Invoke(this, new OnstateChangedEventArgs{ state = this.state});
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgessChangedEventArgs
                    {
                        progressNormalized = fryTime/fryingObjectCo.FryingMax
                    });
                }
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                this.GetKitchenObject().SetkitchenObjectParent(player);
                state = State.Idle;
                OnStateChange?.Invoke(this, new OnstateChangedEventArgs{ state = this.state});
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgessChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
            else
            {
                if (player.GetKitchenObject().TryGetPlate(out plateKitchenObject plate))
                {
                    if (plate.tryAddIngredient(GetKitchenObject().GetKitcheenObjectCO()))
                    {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;
                        OnStateChange?.Invoke(this, new OnstateChangedEventArgs{ state = this.state});
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgessChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
        }
    }
    private bool hasRecipe(KitchenObjectCO kitchenObjectCo)
    {
        FryingObjectCO fryingObjectCO = getFryingObjectCO(kitchenObjectCo);
        if (fryingObjectCO != null)
        {
            return true;
        }

        return false;
    }   
    
    private KitchenObjectCO getOutputForInput(KitchenObjectCO kitchenObjectCo)
    {
        FryingObjectCO fryingObjectCO = getFryingObjectCO(kitchenObjectCo);
        if (fryingObjectCO != null)
        {
            return fryingObjectCO.output;
        }
        return null;
    }

    private FryingObjectCO getFryingObjectCO(KitchenObjectCO kitchenObjectCo)
    {
        foreach (FryingObjectCO recipe in fryingArray){
            if (recipe.input == kitchenObjectCo)
            {
                return recipe;
            }
        }

        return null;
    }
    private BurnObjectCO getburnObjectCO(KitchenObjectCO kitchenObjectCo)
    {
        foreach (BurnObjectCO recipe in burnArray){
            if (recipe.input == kitchenObjectCo)
            {
                return recipe;
            }
        }

        return null;
    }
}
