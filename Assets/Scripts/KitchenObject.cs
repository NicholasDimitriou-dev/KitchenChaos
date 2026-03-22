using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectCO kitchenObjectCo;
    private IKitcehnObjectParent kitchenObjectParent;
    public KitchenObjectCO GetKitcheenObjectCO()
    {
        return kitchenObjectCo;
    }

    public void SetkitchenObjectParent(IKitcehnObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        // this.kitchenObjectParent.ClearKitchenObject();
        if (kitchenObjectParent.HasKitchenObject())
        {
            
            Debug.LogError("already has an object");
            return;
        }
       kitchenObjectParent.SetKitchenObject(this);
       transform.parent = kitchenObjectParent.GetKitchObjectFollowTransfrom();
       transform.localPosition = Vector3.zero;
    }
    public IKitcehnObjectParent GetkitchenObjectParent()
    {
        return kitchenObjectParent;
    }
}
