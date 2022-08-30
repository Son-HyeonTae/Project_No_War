using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : RangeAttackWeaponType
{
    public override void Awake()
    {
        HitRange        = new Vector3(1, 1);
        Angle           = 0;
        ExplosionDelay  = 3;
        Damage          = 80;
        ReuseTime       = 5;
        base.Awake();


        ActionType = eRangeActionType.TIMER;
    }

    public override void Execute(Vector3 Coordinate)
    {

        base.Execute(Coordinate);
        RangeAttack(Boomb);
    }

    private void Boomb(Entity entity)
    {
        entity.TakeHit(data.Damage.Value, eEntityHitType.NORMAL);
        entity.HitAction();
    }
}
