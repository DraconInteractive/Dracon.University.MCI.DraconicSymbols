using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractableDebug : MonoBehaviour
{
    public Interactable interactable;

    private void OnValidate()
    {
        if (interactable == null)
        {
            interactable = GetComponent<Interactable>();
        }
    }
}
