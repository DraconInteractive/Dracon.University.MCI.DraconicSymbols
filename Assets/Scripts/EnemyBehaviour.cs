using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    protected BaseEnemy owner;

    public virtual void Setup (BaseEnemy _owner)
    {
        owner = _owner;
    }

    public virtual void Tick ()
    {

    }
}

