using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVisibilityStep : MonoBehaviour, IStep
{

    [field:SerializeField] public int Id {get; set;}
    [SerializeField] private GameObject selectedGameObject;
    [SerializeField] private bool visibility;
    [SerializeField] private float durationToChangeVisibility = 2f;
    

    public void BeginStep(Step step)
    {
        if(!step.IsSkippable)
        {
            StartCoroutine(ChangeVisibilityCoroutine());
        }
        else
        {
            ChangeVisibility();
        }
    }

    private void ChangeVisibility()
    {
        selectedGameObject.SetActive(visibility);
    }

    private IEnumerator ChangeVisibilityCoroutine()
    {
        yield return new WaitForSeconds(durationToChangeVisibility);

        selectedGameObject.SetActive(visibility);

        SequencingSystem.Instance.CompleteStep();
    }
}
