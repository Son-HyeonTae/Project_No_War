using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : RangeAttackWeaponType
{
    [SerializeField] GameObject hitEffect;

    public override void Awake()
    {
        HitRange        = new Vector3(1, 1);
        Angle           = 0;
        ExplosionDelay  = 3;
        Damage          = 80;
        ReuseTime       = 5;
        base.Awake();

    }

    public override void Execute(Vector3 Coordinate)
    {
        base.Execute(Coordinate);
        RangeAttack(HitAction, HitEffect);
    }

    private void HitAction(Entity entity)
    {
        entity.TakeHit(data.Damage.Value, eEntityHitType.STIFF);
        entity.HitAction();
    }

    private void HitEffect()
    {
        hitEffect = Instantiate(hitEffect);
        hitEffect.transform.position = transform.position;
        StartCoroutine(effectDestroy());
    }

    private IEnumerator effectDestroy()
    {
        Destroy(hitEffect, 0.7f);
        yield return null;
    }
}
