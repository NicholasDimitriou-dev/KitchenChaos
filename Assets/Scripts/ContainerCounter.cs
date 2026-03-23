using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectCO kitchenObjectCo;
    public event EventHandler OnPlayerGrabObject;
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchObject(kitchenObjectCo, player);
            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty); 
        }
    }
}
