using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SequencingSystem : MonoBehaviour
{
    public static SequencingSystem Instance {get; private set;}

    public event Action<Step> OnStepComplete;

    [Header("References"), Space]
    [SerializeField] private SequenceSO sequence;
    [SerializeField] private Transform sequencingHandlerTransform;
    [SerializeField] private GameObject emptyGameObject;

    [field:SerializeField] public int CurrentStepIndex {get; private set;}
    

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void CompleteStep()
    {
        CurrentStepIndex++;

        if(CurrentStepIndex >= sequence.Steps.Count)
        {
            // Finished all the steps of the selected sequence
            return;
        }

        OnStepComplete?.Invoke(sequence.Steps[CurrentStepIndex]);
    }

    public void PlaySequencingSystem()
    {
        CurrentStepIndex = 0;
        OnStepComplete?.Invoke(sequence.Steps[CurrentStepIndex]);
    }


    public void HandleUpdateNewOnesButtonPressed()
    {

        // Spawn the child objects
        foreach(Step step in sequence.Steps)
        {
            if(IsThereStepWithSameId(step.Id))
                {continue;}

            GameObject stepGO = Instantiate(emptyGameObject, sequencingHandlerTransform);

            stepGO.name = step.StepName;
            
            HandleStepType(step, stepGO);
        }
    }

    public void HandleUpdateAllButtonPressed()
    {
        // Clean up - all the child objects
        int childCount = sequencingHandlerTransform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            DestroyImmediate(sequencingHandlerTransform.GetChild(0).gameObject);
        }

        // Spawn the child objects
        foreach(Step step in sequence.Steps)
        {

            GameObject stepGO = Instantiate(emptyGameObject, sequencingHandlerTransform);

            stepGO.name = step.StepName;
            
            HandleStepType(step, stepGO);
        }
    }

    private void HandleStepType(Step step, GameObject stepGO)
    {
        switch (step.StepType)
        {
            case StepType.AnimStep:
                AnimationStep animStep = stepGO.AddComponent<AnimationStep>();
                animStep.Id = step.Id;
                break;
            case StepType.AudioClipStep:
                AudioClipStep audioClipStep = stepGO.AddComponent<AudioClipStep>();
                audioClipStep.Id = step.Id;
                break;
            case StepType.MoveCameraStep:
                MoveCameraStep moveCameraStep = stepGO.AddComponent<MoveCameraStep>();
                moveCameraStep.Id = step.Id;
                break;
            case StepType.GameobjectVisibilityStep:
                ChangeVisibilityStep changeVisibilityStep = stepGO.AddComponent<ChangeVisibilityStep>();
                changeVisibilityStep.Id = step.Id;
                break;
            default:
                break;
        }
    }

    private bool IsThereStepWithSameId(int newStepId)
    {
        int childCount = sequencingHandlerTransform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            if(newStepId == sequencingHandlerTransform.GetChild(i).GetComponent<IStep>().Id)
            {
                return true;
            }
        }
        return false;
    }

}
