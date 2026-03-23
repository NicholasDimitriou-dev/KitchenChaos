using System;
using UnityEngine;


public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] private CuttingObjectCO[] cutKitchenObjectCoArray;
    private int cuttingProgress;
    public event EventHandler<IHasProgress.OnProgessChangedEventArgs> OnProgressChanged;
    public static event EventHandler OnCut;
    public void Start()
    {
        cuttingProgress = 0;
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
                    cuttingProgress = 0;
                    CuttingObjectCO cuttingObjectCo = getCuttingObjectCo(GetKitchenObject().GetKitcheenObjectCO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgessChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress/cuttingObjectCo.cuttingProgressMax
                    });
                }
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                this.GetKitchenObject().SetkitchenObjectParent(player);
            }
            else
            {
                if (player.GetKitchenObject().TryGetPlate(out plateKitchenObject plate))
                {
                    if (plate.tryAddIngredient(GetKitchenObject().GetKitcheenObjectCO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
        
    }

    public override void InteractAlt(Player player)
    {
        if (HasKitchenObject() && hasRecipe(GetKitchenObject().GetKitcheenObjectCO()))
        {
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingObjectCO cuttingObjectCo = getCuttingObjectCo(GetKitchenObject().GetKitcheenObjectCO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgessChangedEventArgs
            {
                progressNormalized = (cuttingProgress)*1f/cuttingObjectCo.cuttingProgressMax
            });
            if (cuttingProgress >= cuttingObjectCo.cuttingProgressMax) {
                KitchenObjectCO kitchenObjectCo = getOutputForInput(GetKitchenObject().GetKitcheenObjectCO());
                GetKitchenObject().DestroySelf(); 
                KitchenObject.SpawnKitchObject(kitchenObjectCo, this);
            }
            
        }
    }

    private bool hasRecipe(KitchenObjectCO kitchenObjectCo)
    {
        CuttingObjectCO cuttingObjectCo = getCuttingObjectCo(kitchenObjectCo);
        if (cuttingObjectCo != null)
        {
            return true;
        }

        return false;
    }   
    
    private KitchenObjectCO getOutputForInput(KitchenObjectCO kitchenObjectCo)
    {
        CuttingObjectCO cuttingObjectCo = getCuttingObjectCo(kitchenObjectCo);
        if (cuttingObjectCo != null)
        {
            return cuttingObjectCo.output;
        }
        return null;
    }

    private CuttingObjectCO getCuttingObjectCo(KitchenObjectCO kitchenObjectCo)
    {
        foreach (CuttingObjectCO recipe in cutKitchenObjectCoArray)
        {
            if (recipe.input == kitchenObjectCo)
            {
                return recipe;
            }
        }

        return null;
    }
}
