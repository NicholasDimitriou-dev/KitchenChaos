using System;
using UnityEngine;
using UnityEngine.Splines;

public class ClearCounter : MonoBehaviour, IKitcehnObjectParent
{
    [SerializeField] private KitchenObjectCO kitchenObjectCo;
    [SerializeField] private Transform location;
    private KitchenObject kitchenObject;
    

    public void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            Transform KOTransform = Instantiate(kitchenObjectCo.prefab, location);
            KOTransform.GetComponent<KitchenObject>().SetkitchenObjectParent(this);
        }
        else { 
            kitchenObject.SetkitchenObjectParent(player);
            // Debug.Log(kitchenObject.GetkitchenObjectParent());
        }
        
    }

    public Transform GetKitchObjectFollowTransfrom()
    {
        return location;
    }

    public void SetKitchenObject(KitchenObject ko)
    {
        this.kitchenObject = ko;
        this.kitchenObjectCo = ko.GetKitcheenObjectCO();
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
