using UnityEngine;
    
public class Delivery : BaseCounter
{
    public static Delivery Instance { get; private set; }


    private void Awake() {
        Instance = this;
    }
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
