using UnityEngine;
[CreateAssetMenu()]
public class CuttingObjectCO : ScriptableObject
{
    public KitchenObjectCO input;
    public KitchenObjectCO output;
    public int cuttingProgressMax;
}
