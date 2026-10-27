using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProductionVisualController : MonoBehaviour
{
    [Header("Production")]
    [SerializeField] private ProductionMachine productionMachine;
    [Header("Conveyor")]
    [SerializeField] private Animator[] conveyorAnimators;
    [Header("MechArmsAnimator")]
    [SerializeField] private Animator[] armAnimators;
    [Header("Anim Settings")]
    [SerializeField] private bool animationOnlyDuringProduction = true;

    private bool lastProductionState;

    private void Start()
    {
        lastProductionState = productionMachine != null && 
            productionMachine.IsProducing;
        UpdateAnimationState(lastProductionState);
    }
    private void Update()
    {
        if (productionMachine == null)
            return;
        bool isProducing = productionMachine.IsProducing;
        if (isProducing == lastProductionState)
            return;
        lastProductionState = isProducing;
        UpdateAnimationState(isProducing);
    }
    private void UpdateAnimationState(bool isProducing)
    {
        float animationSpeed = animationOnlyDuringProduction ? 
            (isProducing ? 1f : 0f) : 1f;
        SetAnimationSpeed(conveyorAnimators, animationSpeed);
        SetAnimationSpeed(armAnimators, animationSpeed);
    }
    private void SetAnimationSpeed(Animator[] animators, float speed)
    {
        if (animators == null) return;
        foreach(Animator animator in animators)
        {
            if (animator != null)
                animator.speed = speed;
        }
    }
}