using System;
using UnityEngine;

public class platesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    [SerializeField] private KitchenObjectCO plate;
    private float spawnPlateTimer;
    private float spawnRate = 4f;
    private int plateNumber;
    private int maxPlates = 4;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        // Debug.Log(spawnPlateTimer);
        if (spawnPlateTimer > spawnRate)
        {
            spawnPlateTimer = 0f;
            if (plateNumber < maxPlates)
            {
                plateNumber++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (plateNumber > 0)
            {
                plateNumber--;
                KitchenObject.SpawnKitchObject(plate, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
        
    }
}
