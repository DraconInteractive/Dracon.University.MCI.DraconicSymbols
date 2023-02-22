using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WealthManager : MonoBehaviour
{
    public static WealthManager Instance;

    public float currentWealth;

    private void Awake()
    {
        Instance = this;
    }

    public void IncrementWealth (int amount)
    {
        currentWealth += amount;
    }
}
