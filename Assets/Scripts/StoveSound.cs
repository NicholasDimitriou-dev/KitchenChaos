using System;
using UnityEngine;

public class StoveSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private Stove stove;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Stove.OnStateChange += Stove_stateChange;
    }

    private void Stove_stateChange(object sender, Stove.OnstateChangedEventArgs e)
    {
        bool playSound = e.state == Stove.State.Frying || e.state == Stove.State.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
