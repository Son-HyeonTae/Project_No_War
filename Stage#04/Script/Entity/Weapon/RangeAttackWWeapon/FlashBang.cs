using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FlashBang : RangeAttackWeaponType
{
    private Image Flash;

    public override void Awake()
    {
        HitRange        = new Vector3(1, 1);
        Angle           = 0;
        ExplosionDelay  = 0;
        Damage          = 20;
        ReuseTime       = 5;
        base.Awake();


        Flash = GameObject.Find("FlashBangUI").GetComponent<Image>();
        Flash.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
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
        /*Flash.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        StartCoroutine(GameManager.Instance.FadeOut(0.02f, 0.0f, Flash, ()
            => {
                StartCoroutine(GameManager.Instance.FadeIn(0.7f, 0.0f, Flash));
            }));
*/
    }
}