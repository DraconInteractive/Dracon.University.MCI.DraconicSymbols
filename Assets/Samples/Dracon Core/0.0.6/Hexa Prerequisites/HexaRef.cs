using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaRef : MonoBehaviour
{
    public static HexaRef Instance;
    
    private void Awake()
    {
        Instance = this;
    }


}
