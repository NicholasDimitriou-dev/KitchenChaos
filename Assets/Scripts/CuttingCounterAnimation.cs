using System;
using UnityEngine;

public partial class ContainerCounterAnimation : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    private const string CUT = "Cut";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CuttingCounter.OnCut += Cutting_OnCut;
    }

    private void Cutting_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
