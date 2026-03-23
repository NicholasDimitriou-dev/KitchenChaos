using System;
using UnityEngine;

public class StoveParticalSystem : MonoBehaviour
{
    [SerializeField] private stove stove;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particalGameObject;

    private void Start()
    {
        stove.OnStateChange += Stove_stateChange;
    }

    private void Stove_stateChange(object sender, stove.OnstateChangedEventArgs e)
    {
        bool showVisual = e.state == stove.State.Frying || e.state == stove.State.Fried;
        stoveOnGameObject.SetActive(showVisual);
        particalGameObject.SetActive(showVisual);
    }
}

