using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mob
{
    public bool bDisplayGizmos;

    public override void Awake() { base.Awake(); }
    public override void Start() { base.Start(); }

    private void OnEnable()
    {
        StateMachine.Change(StateMachine.MoveState);
        Init();
    }

    private void OnDrawGizmos()
    {
        if(bDisplayGizmos)
        {
            Gizmos.DrawWireCube(transform.position, Vector3.one * 2);
        }
    }

    public override void RequestDespawn()
    {
        data.bDead = true;
        ObjectPoolStorage.Instance.Pool_Enemy.Despawn(this);
    }
}
