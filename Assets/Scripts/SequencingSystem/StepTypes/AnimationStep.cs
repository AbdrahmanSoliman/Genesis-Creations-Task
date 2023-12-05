using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStep : MonoBehaviour, IStep
{
    [field:SerializeField] public int Id {get; set;}
    [SerializeField] private Animator animator;
    [SerializeField] private string animationName;
    
    private AnimationClip selectedAnimationClip;

    public void BeginStep(Step step)
    {
        if(!HasAnimationClipName())
        {
            Debug.LogError($"Couldn't find animation clip {animationName} in the selected animator {animator.name}");
            return;
        }

        if(!step.IsSkippable)
        {
            StartCoroutine(PlayAnimationCoroutine());
        }
        else
        {
            PlayAnimation();
        }
    }


    private bool HasAnimationClipName()
    {
        AnimationClip[] animationClips = animator.runtimeAnimatorController.animationClips;

        foreach(AnimationClip clip in animationClips)
        {
            if(clip.name == animationName)
            {
                selectedAnimationClip = clip;
                return true;
            }
        }

        return false;
    }

    private float GetAnimationClipLength()
    {
        return selectedAnimationClip.length;
    }
    

    private void PlayAnimation()
    {
        animator.Play(animationName);
    }

    private IEnumerator PlayAnimationCoroutine()
    {
        animator.Play(animationName);

        yield return new WaitForSeconds(GetAnimationClipLength());

        SequencingSystem.Instance.CompleteStep();
    }
}
