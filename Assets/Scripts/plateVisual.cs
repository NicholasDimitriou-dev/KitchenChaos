using System;
using System.Collections.Generic;
using UnityEngine;

public class plateVisual : MonoBehaviour
{
    [SerializeField] private platesCounter platesCounter;
    [SerializeField] private Transform location;
    [SerializeField] private Transform plateVisualLocal;
    private List<GameObject> plateVisualList;

    private void Awake()
    {
        plateVisualList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlateCounter_OnPlatesSpawned;
        platesCounter.OnPlateRemoved += PlateCounter_OnPlatesRemoved;
    }

    private void PlateCounter_OnPlatesRemoved(object sender, EventArgs e)
    {
        GameObject plateGameObject = plateVisualList[plateVisualList.Count - 1];
        plateVisualList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlateCounter_OnPlatesSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualLocal, location);
        float plateOffset = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffset * plateVisualList.Count, 0);
        plateVisualList.Add(plateVisualTransform.gameObject);
    }
}
