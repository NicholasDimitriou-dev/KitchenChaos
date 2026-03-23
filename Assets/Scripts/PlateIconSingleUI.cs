using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;
    public void SetKitchenObject(KitchenObjectCO kitchenObjectCo)
    {
        image.sprite = kitchenObjectCo.sprite;
    }
}
