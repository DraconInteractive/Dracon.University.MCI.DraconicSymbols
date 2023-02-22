using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InteractableDebug))]
public class InteractableDebugEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Interactable i = (target as InteractableDebug).interactable;

        if (GUILayout.Button("Interact"))
        {
            i.Interact();
        }

        if (GUILayout.Button("Grab L"))
        {
            i.Grab(Interactor.left);
        }
        
        if (GUILayout.Button("Grab R"))
        {
            i.Grab(Interactor.right);
        }

        if (GUILayout.Button("Release"))
        {
            i.Release(i.interactor);
        }
    }
}
