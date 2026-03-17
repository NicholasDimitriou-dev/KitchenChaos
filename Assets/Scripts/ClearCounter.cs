using System;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectCO kitchenObjectCo;
    [SerializeField] private Transform location;
    private KitchenObject kitchenObject;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool test;

    private void Update()
    {
        if (test && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
                this.kitchenObject = null;
                
            }
        }
    }

    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform KOTransform = Instantiate(kitchenObjectCo.prefab, location);
            KOTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(kitchenObject.GetClearCounter());
        }
        
    }

    public Transform GetKitchObjectFollowTransfrom()
    {
        return location;
    }

    public void SetKitchenObject(KitchenObject ko)
    {
        this.kitchenObject = ko;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
        this.kitchenObjectCo = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
