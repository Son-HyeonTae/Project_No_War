using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : RangeAttackWeaponType
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Execute(Vector3 Coordinate)
    {

        base.Execute(Coordinate);
        RangeAttack(Boomb);
    }

    private void Boomb(Entity entity)
    {
        entity.TakeHit(data.Damage.Value);
        entity.HitAction();
    }
}
