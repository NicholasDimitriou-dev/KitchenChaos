using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryMangerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recepeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconImage;

    private void Awake()
    {
        iconImage.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeCO recipeCo)
    {
        recepeNameText.text = recipeCo.recipeName;
        foreach (Transform child in iconContainer)
        {
            if (child == iconImage)
            {
                continue;
            }
            else
            {
                Destroy(child.gameObject);
            }
        }

        foreach (KitchenObjectCO kitchenObjectCo in recipeCo.kitchenObjectCoList)
        {
            Transform iconTransform =Instantiate(iconImage, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectCo.sprite;
        }
    }
}
