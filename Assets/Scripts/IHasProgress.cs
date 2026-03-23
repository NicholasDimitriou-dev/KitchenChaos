using System;
using UnityEngine;

public interface IHasProgress {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public event EventHandler<OnProgessChangedEventArgs> OnProgressChanged;
    

    public class OnProgessChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
}
