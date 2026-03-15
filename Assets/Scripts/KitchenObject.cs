using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectCO kitchenObjectCo;
    private ClearCounter clearCounter;
    public KitchenObjectCO GetKitcheenObjectCO()
    {
        return kitchenObjectCo;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("already has an object");
        }
       clearCounter.SetKitchenObject(this);
       transform.parent = clearCounter.GetKitchObjectFollowTransfrom();
       transform.localPosition = Vector3.zero;
    }
    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
