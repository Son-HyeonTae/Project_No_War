using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBang : RangeAttackWeaponType
{
    [SerializeField] private float FaintTime;

    public override void Awake()
    {
        base.Awake();
        Damage = 1;
        ReuseTime = 5;
        Amount = 99999;
        bImmediateStart = false;
    }

    public override void Execute(Vector3 Coordinate)
    {
        base.Execute(Coordinate);
        RangeAttack(Boomb);
    }

    private void Boomb(Entity entity)
    {
        
        entity.HitAction();
    }
}

////////Enemy Statement ÇÊ¿ä