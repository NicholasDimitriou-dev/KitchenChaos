using UnityEngine;

public interface IKitcehnObjectParent
{
    public Transform GetKitchObjectFollowTransfrom();

    public void SetKitchenObject(KitchenObject ko);

    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();
}
