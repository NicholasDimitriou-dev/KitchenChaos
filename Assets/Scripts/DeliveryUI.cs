using System;
using UnityEngine;

public class DeliveryUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTamplate;

    private void Awake()
    {
        // recipeTamplate.gameObject.SetActive(false);
        DeliveryManager.Instance.OnRecipeSpawned += Instance_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += Instance_OnRecipeCompleted;
        UpdateVisual();
    }

    private void Instance_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void Instance_OnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTamplate)
            {
                continue;
            }
            else
            {
                Destroy(child.gameObject);
            }
        }

        foreach (RecipeCO recipeCo in DeliveryManager.Instance.GetWaitingRecipeCOList())
        {
            Transform recipeTransform = Instantiate(recipeTamplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryMangerSingleUI>().SetRecipeSO(recipeCo);
        }
    }
}
