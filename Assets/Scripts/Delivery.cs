using UnityEngine;
    
public class Delivery : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out plateKitchenObject plate))
            {
                DeliveryManager.Instance.DeliverRecipe(plate);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
