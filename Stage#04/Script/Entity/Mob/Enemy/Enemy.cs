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
        StateMachine.Initialize(StateMachine.MoveState);
        Init();
    }

    public override void RequestDespawn()
    {
        data.bDead = true;
        ObjectPoolStorage.Instance.Pool_Enemy.Despawn(this);
    }
}
