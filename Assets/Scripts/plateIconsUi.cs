using System;
using UnityEngine;

public class plateIconsUi : MonoBehaviour
{
    [SerializeField] private plateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += plateKitchenObject_OnIngredientAdded;
    }

    private void plateKitchenObject_OnIngredientAdded(object sender, plateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectCO kitchenObjectCo in plateKitchenObject.GetKitchenObjectCOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTemplate.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObject(kitchenObjectCo);
            

        }
    }
}
