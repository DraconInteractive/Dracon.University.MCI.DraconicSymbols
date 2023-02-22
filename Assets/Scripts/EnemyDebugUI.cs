using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDebugUI : MonoBehaviour
{
    BaseEnemy enemy;
    public Image detectionFill;

    private void Start()
    {
        enemy = GetComponent<BaseEnemy>();    
    }

    private void Update()
    {
        detectionFill.fillAmount = enemy.CurrentDetection;
    }
}
