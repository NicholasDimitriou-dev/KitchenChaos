using System;
using UnityEngine;
using UnityEngine.Splines;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectCO kitchenObjectCo;
    
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetkitchenObjectParent(this);
            }
        } else {
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out plateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.tryAddIngredient(GetKitchenObject().GetKitcheenObjectCO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        if (plateKitchenObject.tryAddIngredient(player.GetKitchenObject().GetKitcheenObjectCO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } else {
                GetKitchenObject().SetkitchenObjectParent(player);
            }
        }
    }
    
}
