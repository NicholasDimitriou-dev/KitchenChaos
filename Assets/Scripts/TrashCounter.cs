using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler trashing;
    new public static void ResetStaticData() {
       trashing = null;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            trashing?.Invoke(this,EventArgs.Empty);
            
        }
    }
}
