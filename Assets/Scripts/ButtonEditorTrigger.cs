using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEditorTrigger : MonoBehaviour
{
    #if UNITY_EDITOR
    [Button]
    public void Press () 
    {
        GetComponent<Button>().onClick?.Invoke();
    }
    #endif
}
