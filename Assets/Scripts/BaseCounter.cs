using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitcehnObjectParent
{
    public static event EventHandler OnDrop;
    [SerializeField] private Transform location;
    private KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {
        Debug.Log("How?");
    }
    public virtual void InteractAlt(Player player)
    {
        Debug.Log("How?");
    }
    public Transform GetKitchObjectFollowTransfrom()
    {
        return location;
    }

    public void SetKitchenObject(KitchenObject ko)
    {
        this.kitchenObject = ko;
        if (kitchenObject != null)
        {
            OnDrop?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
        // this.kitchenObjectCo = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
