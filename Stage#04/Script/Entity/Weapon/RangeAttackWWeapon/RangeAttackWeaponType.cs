using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 범위형 공격 Model
/// 하위 클래스에서 상속받아 사용
/// 상속받은 클래스는 base.Execute사용
/// RangeAttack Coroutine의 Action 매개변수에 적용할 효과 구현하여 사용
/// ->         StartCoroutine(RangeAttack(Action<>));
/// </summary>

public class RangeAttackWeaponType : Weapon
{
    //--var
    [SerializeField] protected float ExplosionDelay;
    [SerializeField] protected float Damage;
    [SerializeField] protected float ReuseTime;
    [SerializeField] protected int Amount;
    [SerializeField] protected bool bImmediateStart;

    [SerializeField] protected Vector2 HitRange;
    [SerializeField] protected float Angle;

    protected BoxCollider2D Collider;
    protected Collider2D[] TagObjects;


    //---Gizmos
     [SerializeField] protected bool bDisplayGizmos;

    public virtual void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        data.Damage.Value = Damage;
        data.Cooltime.Value = ReuseTime;
        data.Amount.Value = Amount;
        data.bImmediateStart = bImmediateStart;
        Collider = GetComponent<BoxCollider2D>();
        Collider.enabled = false;
        HitRange = Vector2.one;
        Angle = 0.0f;
    }

    public override void Execute(Vector3 Coordinate)
    {
        transform.position = Coordinate;
    }

    public void RangeAttack(Action<Entity> action)
    {
        StartCoroutine(RangeAttackCor(action));
    }
    public IEnumerator RangeAttackCor(Action<Entity> action)
    {
        yield return new WaitForSeconds(ExplosionDelay);
        Collider.enabled = true;
        TagObjects = Physics2D.OverlapBoxAll(transform.position, HitRange, Angle);
        //A better idea than this?  Optimization
        foreach (var go in TagObjects)
        {
            if (go.TryGetComponent(out Entity entity))
            {
                action(entity);
            }
        }

        yield return null;
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if(bDisplayGizmos)
        {
            Gizmos.DrawCube(transform.position, HitRange);
        }
    }
}
