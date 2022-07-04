using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : Entity
{

    [SerializeField] protected float Damage;
    [SerializeField] protected float Velocity;

    //--base
    public virtual void Execute() { }
}
