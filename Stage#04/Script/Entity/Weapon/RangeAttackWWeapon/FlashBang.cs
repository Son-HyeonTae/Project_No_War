using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBang : RangeAttackWeaponType
{
    [SerializeField] private DBPFaint faint;

    public override void Awake()
    {
        HitRange        = new Vector3(1, 1);
        Angle           = 0;
        ExplosionDelay  = 0;
        Damage          = 20;
        ReuseTime       = 5;
        base.Awake();


        ActionType = eRangeActionType.POINT;
    }

    public override void Execute(Vector3 Coordinate)
    {
        base.Execute(Coordinate);
        RangeAttack(Boomb);
    }

    private void Boomb(Entity entity)
    {
        entity.TakeHit(Damage, eEntityHitType.FAINT);
        entity.HitAction();
    }
}