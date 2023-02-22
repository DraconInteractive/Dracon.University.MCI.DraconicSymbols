using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class FAction_PlayAudioClip : FlowAction
{
    public bool useGlobalAudio = true;

    [HideIf("useGlobalAudio")]
    public AudioSource audioSource;
    public AudioClip clip;

    public bool waitForFinish = true;
    [ShowIf("waitForFinish")]
    public float spacer = 0.05f;

    public bool playOneShot = true;

    public override void Begin()
    {
        base.Begin();

        instigator.StartCoroutine(Run());
    }

    IEnumerator Run ()
    {
        if (useGlobalAudio)
        {
            audioSource = GlobalAudio.Instance.audioSource;
        }

        if (playOneShot)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }

        if (waitForFinish)
        {
            yield return new WaitForSeconds(clip.length + spacer);
        }

        Complete();
        yield break;
    }
}
