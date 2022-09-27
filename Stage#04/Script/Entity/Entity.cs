using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/**
* 피해를 입을 수 있는 모든 객체를 포함하는 클래스
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public interface IDamgeable
{
    void TakeHit(float damage, eEntityHitType HitType);
}

///피해 타입 열거체
public enum eEntityHitType
{
    NORMAL,
    STIFF,
    FAINT
}

public class Entity : MonoBehaviour, IDamgeable
{
    protected eEntityHitType MyHitType { get; set; }

    ///=================================
    ///     Reference Self Components
    ///=================================
    protected Entity     Self                  { get; private set; }
    protected Collider2D ColliderComponentSelf { get; private set; }

    
    public EntityData           data            { get; set; }
    public Rigidbody2D          Rb2D            { get; private set; }
    public StateMachine         StateMachine    { get; private set; }
    public PathFindHelper       PathFindSystem  { get; private set; }
    public EnemyAnimController  AnimController  { get; private set; }

    ///=================================
    ///      Virtual Functions
    ///=================================
    public virtual void HitAction() { }
    public virtual void RequestDespawn() { }

    public virtual void Awake()
    {
        data                    = ScriptableObject.CreateInstance<EntityData>();
        StateMachine            = GetComponent<StateMachine>();
        ColliderComponentSelf   = GetComponent<BoxCollider2D>();
        PathFindSystem          = GetComponent<PathFindHelper>();
        Rb2D                    = GetComponent<Rigidbody2D>(); 
        AnimController          = GetComponent<EnemyAnimController>();
        Init();


        PathFindSystem.Velocity = data.Velocity.Value;
    }


    public void Init()
    {
        data.baseHP.Value   = 100;
        data.Velocity.Value = 1;
        data.bDead          = false;
        data.HP.Value       = data.baseHP.Value;
    }


    /**
    * 자신에게 데미지 + 상태이상을 부여하는 함수
    * 적 스크립트에서 호출 
    * StateMachine을 통한 상태 전이도 함께 수행
    * 
    * @param float Damage - 입을 피해
    * @param eEntityHitType HitType - 피해 타입
    * @return null
    * @exception 
    */
    public void TakeHit(float damage, eEntityHitType HitType)
    {
        data.HP.Value -= damage;

        switch (HitType)
        {
            case eEntityHitType.NORMAL:
                break;
            case eEntityHitType.FAINT:
                StateMachine.Change(StateMachine.FaintState);
                break;
            case eEntityHitType.STIFF:
                StateMachine.Change(StateMachine.StiffState);
                break;
            default:
                break;
        }


        if (data.HP.Value <= 0 && !data.bDead)
        {
            Die();
        }
    }

    private void Die()
    {
        RequestDespawn();
    }
}
