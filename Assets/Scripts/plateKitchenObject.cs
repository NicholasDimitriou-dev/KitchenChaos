using System;
using System.Collections.Generic;
using UnityEngine;

public class plateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectCO kitchenObjectCo;
    }
    [SerializeField] private List<KitchenObjectCO> valid;
    private List<KitchenObjectCO> kitchenObjectList;

    private void Awake()
    {
        kitchenObjectList = new List<KitchenObjectCO>();
    }

    public bool tryAddIngredient(KitchenObjectCO kitchenObjectCo)
    {
        if (!valid.Contains(kitchenObjectCo))
        {
            return false;
        }
        if (kitchenObjectList.Contains(kitchenObjectCo))
        {
            return false;
        }else {
            kitchenObjectList.Add(kitchenObjectCo);
            OnIngredientAdded?.Invoke(this,new OnIngredientAddedEventArgs
            {
                kitchenObjectCo = kitchenObjectCo
            });
            return true;  
        }
        
    }

    public List<KitchenObjectCO> GetKitchenObjectCOList()
    {
        return kitchenObjectList;
    }
}
