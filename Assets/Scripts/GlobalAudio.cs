using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudio : MonoBehaviour
{
    public static GlobalAudio Instance;
    public AudioSource audioSource;

    private void OnValidate()
    {
        if (audioSource == null)
        {
            GetComponent<AudioSource>();
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
