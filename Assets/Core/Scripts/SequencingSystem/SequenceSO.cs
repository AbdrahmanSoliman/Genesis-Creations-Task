using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StepType
{
    AnimStep, AudioClipStep, MoveCameraStep, GameobjectVisibilityStep 
}


[CreateAssetMenu(fileName = "sequence", menuName = "Sequence/New Sequence")]
public class SequenceSO : ScriptableObject
{
    public List<Step> Steps;
}


[System.Serializable]
public struct Step
{
    [Header("Step Info"), Space]
    public string StepName;
    public StepType StepType;
    public int Id;
    public bool IsSkippable;

}
