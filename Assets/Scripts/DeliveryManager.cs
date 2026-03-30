using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFail;
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSo;
    public List<RecipeCO> waitingRecipe;
    private int successRecipe;
    private float spawnTime;
    private float spawnTimeMax = 4f;
    private int waitingMax = 4;

    private void Awake()
    {
        Instance = this;
        waitingRecipe = new List<RecipeCO>();
    }

    private void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime >= spawnTimeMax)
        {
            spawnTimeMax = 0f;
            if (waitingRecipe.Count < waitingMax)
            {
                RecipeCO waitingRecipeSO = recipeListSo.recipeListCO[Random.Range(0, recipeListSo.recipeListCO.Count)];
                // Debug.Log(waitingRecipeSO.name);
                waitingRecipe.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }

    public void DeliverRecipe(plateKitchenObject plate)
    {
        for(int i = 0; i < waitingRecipe.Count;i++)
        {
            RecipeCO waitingRecipeCO = waitingRecipe[i];
            if (waitingRecipeCO.kitchenObjectCoList.Count == plate.GetKitchenObjectCOList().Count)
            {
                bool plateContentsMatch = true;
                foreach (KitchenObjectCO kitchenObjectCo in waitingRecipeCO.kitchenObjectCoList)
                {
                    bool foundIngred = false;
                    foreach (KitchenObjectCO platekitchenObjectCo in plate.GetKitchenObjectCOList())
                    {
                        if (platekitchenObjectCo == kitchenObjectCo)
                        {
                            foundIngred = true;
                            break;
                        }
                    }

                    if (!foundIngred)
                    {
                        plateContentsMatch = false;
                    }
                }

                if (plateContentsMatch)
                {
                    successRecipe++;
                    Debug.Log("CorrectRecipe");
                    waitingRecipe.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        OnRecipeFail?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeCO> GetWaitingRecipeCOList()
    {
        return waitingRecipe;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successRecipe;
    }
}
