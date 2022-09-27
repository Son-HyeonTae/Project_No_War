using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FlashBang : RangeAttackWeaponType
{
    FlashBangUIAction UIAction;

    public override void Awake()
    {
        HitRange        = new Vector3(3, 3);
        Angle           = 0;
        ExplosionDelay  = 0;
        Damage          = 20;
        ReuseTime       = 5;
        base.Awake();


        UIAction = GameObject.Find("FlashBangUI").GetComponent<FlashBangUIAction>();
    }

    public override void Execute(Vector3 Coordinate)
    {
        base.Execute(Coordinate);
        RangeAttack(HitAction, HitEffect);
    }

    private void HitAction(Entity entity)
    {
        entity.TakeHit(data.Damage.Value, eEntityHitType.FAINT);
        entity.HitAction();
    }

    private void HitEffect()
    {
        //StartCoroutine(GameManager.Instance.FadeOut(0.2f, 0.0f, Flash));
        UIAction.Flash();
    }

    
}