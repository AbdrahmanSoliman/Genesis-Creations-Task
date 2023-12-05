using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipStep : MonoBehaviour, IStep
{
    [field:SerializeField] public int Id {get; set;}
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;

    public void BeginStep(Step step)
    {
        audioSource.clip = audioClip;

        if(!step.IsSkippable)
        {
            StartCoroutine(PlayAudioClipCoroutine());
        }
        else
        {
            PlayAudioClip();
        }
    }

    private void PlayAudioClip()
    {
        audioSource.Play();
    }

    private IEnumerator PlayAudioClipCoroutine()
    {
        audioSource.Play();

        yield return new WaitForSeconds(audioClip.length);

        SequencingSystem.Instance.CompleteStep();
    }
}
