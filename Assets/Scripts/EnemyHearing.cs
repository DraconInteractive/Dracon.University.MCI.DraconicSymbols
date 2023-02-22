using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyHearing : MonoBehaviour
{
    public Transform ears;

    PlayerSoundModule playerSoundModule;

    public Queue<float> detectedSounds = new Queue<float>();
    private void OnEnable()
    {
        playerSoundModule = PlayerSoundModule.Instance;
        playerSoundModule.onPlaySound += SoundHandler;
    }

    private void OnDisable()
    {
        playerSoundModule.onPlaySound -= SoundHandler;
    }

    public void SoundHandler (float magnitude)
    {
        float distToPlayer = Vector3.Distance(ears.position, Dracon.Core.ReferenceManager.Instance.playerHead.position);
        float level = Mathf.Clamp(magnitude - distToPlayer, 0.0f, magnitude);
        detectedSounds.Enqueue(level);
    }
}
