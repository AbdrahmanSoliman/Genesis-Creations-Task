using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencingSystem : MonoBehaviour
{
    public static SequencingSystem Instance {get; private set;}

    public event Action<Step> OnStepComplete;

    [SerializeField] private SequenceSO sequence;

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

    private void Start()
    {
        CurrentStepIndex = 0;
        OnStepComplete?.Invoke(sequence.Steps[CurrentStepIndex]);
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
}
