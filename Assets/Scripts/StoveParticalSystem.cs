using System;
using UnityEngine;

public class StoveParticalSystem : MonoBehaviour
{
    [SerializeField] private Stove stove;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particalGameObject;

    private void Start()
    {
        Stove.OnStateChange += Stove_stateChange;
    }

    private void Stove_stateChange(object sender, Stove.OnstateChangedEventArgs e)
    {
        bool showVisual = e.state == Stove.State.Frying || e.state == Stove.State.Fried;
        stoveOnGameObject.SetActive(showVisual);
        particalGameObject.SetActive(showVisual);
    }
}

