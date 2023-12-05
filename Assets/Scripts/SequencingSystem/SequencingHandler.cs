using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencingHandler : MonoBehaviour
{
    private IStep[] steps;
    private IStep currentStep;

    private void Awake()
    {
        steps = GetComponentsInChildren<IStep>();
    }

    private void Start()
    {
        SequencingSystem.Instance.OnStepComplete += HandleNewStep;
    }

    private void OnDestroy()
    {
        SequencingSystem.Instance.OnStepComplete -= HandleNewStep;
    }

    private void HandleNewStep(Step newStep)
    {
        currentStep = steps[newStep.Id];

        if(currentStep.Id != newStep.Id) 
        {
            Debug.LogError($"Invalid Step ID! The sequence has been discarded. Info: index {newStep.Id} in the Scriptable Object list and game object doesn't match"); 
            return; 
        }

        currentStep.BeginStep(newStep);

        HandleIsSkippableCondition(newStep);
    }

    private void HandleIsSkippableCondition(Step newStep)
    {
        if(newStep.IsSkippable)
        {
            SequencingSystem.Instance.CompleteStep();
        }
    }
}
