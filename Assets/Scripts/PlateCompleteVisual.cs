using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [SerializeField] private plateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectCo_GameObject> kitchenObjectCoGameObjectList;
    
    [Serializable]
    public struct KitchenObjectCo_GameObject
    {
        public KitchenObjectCO kitchenObjectCo;
        public GameObject gameObject;
    }
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += plateKitchenObject_OnIngredientAdded;
        foreach (KitchenObjectCo_GameObject kitchenObjectCoGameObject in kitchenObjectCoGameObjectList)
        {
            kitchenObjectCoGameObject.gameObject.SetActive(false);
        }
    }
    

    private void plateKitchenObject_OnIngredientAdded(object sender,plateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectCo_GameObject kitchenObjectCoGameObject in kitchenObjectCoGameObjectList)
        {
            if (kitchenObjectCoGameObject.kitchenObjectCo == e.kitchenObjectCo)
            {
                kitchenObjectCoGameObject.gameObject.SetActive(true);
            }
        }
    }
}
