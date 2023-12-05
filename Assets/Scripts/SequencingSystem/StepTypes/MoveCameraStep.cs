using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MoveCameraStep : MonoBehaviour, IStep
{ 
    [field:SerializeField] public int Id {get; set;}
    [SerializeField] private CinemachineVirtualCamera firstVirtualCam;
    [SerializeField] private CinemachineVirtualCamera secondVirtualCam;
    [SerializeField] private float duration = 5f;
    

    public void BeginStep(Step step)
    {
        CinemachineBrain cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();        

        if(cinemachineBrain == null || firstVirtualCam == null || secondVirtualCam == null)
        {
            Debug.LogError("You must add both the first and second virtual camera! The sequence has been discarded");
            return;
        }

        cinemachineBrain.m_DefaultBlend.m_Time = duration;

        
        StartCoroutine(CameraTransitioningCoroutine(step));
    }

    private IEnumerator CameraTransitioningCoroutine(Step step)
    {
        firstVirtualCam.Priority = 10;
        secondVirtualCam.Priority = 20;

        yield return new WaitForSeconds(duration);

        if(!step.IsSkippable)
        {
            SequencingSystem.Instance.CompleteStep();
        }
    }
    
}