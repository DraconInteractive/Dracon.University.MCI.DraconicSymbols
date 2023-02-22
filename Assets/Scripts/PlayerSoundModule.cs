using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundModule : MonoBehaviour
{
    public static PlayerSoundModule Instance { get; private set;}

    public delegate void PlaySoundEvent(float magnitude);
    public PlaySoundEvent onPlaySound;

    private void Awake()
    {
        Instance = this;    
    }

    public void PlaySound (float magnitude)
    {
        onPlaySound?.Invoke(magnitude);
    }
}
