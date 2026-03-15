using System;
using UnityEngine;

public class SetCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visual;
    private void Start() {
        Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
    }

    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        if (e.selectedCounter == clearCounter)
        {
            Show();
        }else {
           Hide(); 
        }
        
    }

    private void Show()
    {
        visual.SetActive(true);
    }

    private void Hide()
    {
        visual.SetActive(false);
    }
}
