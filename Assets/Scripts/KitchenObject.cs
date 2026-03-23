using System;
using Unity.VisualScripting;
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

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(this.gameObject);
    }

    public static KitchenObject SpawnKitchObject(KitchenObjectCO kitchenObjectCo,
        IKitcehnObjectParent kitcehnObjectParent)
    {
        Transform KOTransform = Instantiate(kitchenObjectCo.prefab);
        KitchenObject kitchenObject = KOTransform.GetComponent<KitchenObject>();
        kitchenObject.SetkitchenObjectParent(kitcehnObjectParent);
        return kitchenObject;
    }

    public bool TryGetPlate(out plateKitchenObject plate)
    {
        if (this is plateKitchenObject)
        {
            plate = this as plateKitchenObject;
            return true;
        }
        else
        {
            plate = null;
            return false;
        }
        
    }
}
