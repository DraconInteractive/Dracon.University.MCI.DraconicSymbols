using System;
using System.Collections;
using System.Collections.Generic;
using Dracon.Core.Player;
using UltimateXR.Manipulation;
using UnityEngine;

public class Stealable : MonoBehaviour
{
    public int itemValue;
    public GameObject destroyVFX;
    public GameObject destroySFX;

    private Interactable _interactable;
    private UxrGrabbableObject _grabbable;
    
    private void Start()
    {
        #if UXR
        _grabbable = GetComponent<UxrGrabbableObject>();
        _grabbable.Grabbed += UxrGrab;
        #else
            _interactable = GetComponent<Interactable>();
            _interactable.onGrab.AddListener(CustomGrab);
        #endif

    }

    void BaseGrab()
    {
        WealthManager.Instance.IncrementWealth(itemValue);
        var vfx = Instantiate(destroyVFX, transform.position, Quaternion.identity);
        vfx.transform.localScale = Vector3.one * 0.25f;
        var sfx = Instantiate(destroySFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject, 0.15f);
    }
    
    void UxrGrab(object obj, UxrManipulationEventArgs e)
    {
        BaseGrab();
        
        UxrGrabManager.Instance.ReleaseGrabs(e.GrabbableObject, true);
    }

    void CustomGrab(Interactor grabber)
    {
        BaseGrab();

        grabber.Release();
    }
}
