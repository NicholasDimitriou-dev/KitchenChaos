using System;
using UnityEngine;
using UnityEngine.Splines;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectCO kitchenObjectCo;
    

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetkitchenObjectParent(this);
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
                else
                {
                    if (this.GetKitchenObject().TryGetPlate(out plate))
                    {
                        if (plate.tryAddIngredient(GetKitchenObject().GetKitcheenObjectCO()))
                        {
                            Debug.Log("idk");
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
        }
        
    }
    
}
