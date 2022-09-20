using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
* Weapon을 상속받는 범위형 공격 기본 클래스
* 범위형공격은 이 클래스를 상속받아 사용함
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class RangeAttackWeaponType : Weapon
{
    public enum eRangeActionType
    {
        POINT,
        TIMER,
        INVOCATION,
        NONE
    }

    ///===================================================
    ///            외부 인스펙터 초기화-> base.data에 할당
    ///===================================================
    [SerializeField] protected int Amount;
    [SerializeField] protected bool bImmediateStart;
    [SerializeField] protected eRangeActionType ActionType;
    [SerializeField] protected Transform PlayerComponent;
    [SerializeField] protected float Damage;


    ///===================================================
    ///            RangeAttack Variables
    ///===================================================
    public Vector3  TargetPos       { get; private set; }
    public Vector2  HitRange        { get; protected set; }
    public float    Angle           { get; protected set; }
    public float    ExplosionDelay  { get; protected set; }
    public float    ReuseTime       { get; protected set; }


    //---Gizmos---
    [SerializeField] protected bool bDisplayGizmos;

    ///===================================================
    ///|            Range Type Variables
    ///===================================================

    private RangeAttackType CurrentType;
    private RangeAttack_TYPE_TIMER  Type_TIMER;
    private RangeAttack_TYPE_POINT  Type_POINT;

    ///===================================================
    ///|            Public Method
    ///===================================================
    public virtual void Awake()
    {
        Init();

        Type_TIMER = gameObject.AddComponent<RangeAttack_TYPE_TIMER>();
        Type_POINT = gameObject.AddComponent<RangeAttack_TYPE_POINT>();
    }

    public virtual void Update()
    {
        if(CurrentType != null)
        {
            CurrentType.OnUpdate();
        }
    }

    public override void Init()
    {
        base.Init();
        data.Damage.Value       = Damage;
        data.Cooltime.Value     = ReuseTime;
        data.Amount.Value       = Amount;
        data.bImmediateStart    = bImmediateStart;
    }

    public override void Execute(Vector3 Coordinate)
    {
        base.Execute(Coordinate);
        TargetPos = Coordinate;
    }

    public void RangeAttack(Action<Entity> action, Action effect)
    {
        switch(ActionType)
        {
            case eRangeActionType.TIMER:
                CurrentType = Type_TIMER;
                break;
            case eRangeActionType.POINT:
                CurrentType = Type_POINT;
                break;
            default:
                break;
        }
        if (CurrentType) CurrentType.OnEnter(action, effect);
    }


    ///===================================================
    ///|                    Gizmos
    ///===================================================
    private void OnDrawGizmos()
    {
        if(bDisplayGizmos)
        {
            Gizmos.DrawCube(transform.position, HitRange);
        }
    }

}

/**
* RangeAttack의 Type 기본 클래스
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class RangeAttackType : MonoBehaviour
{
    protected Action                    HitEffect;
    protected Action<Entity>            HitAction;
    protected Collider2D[]              TagObjects;
    protected BoxCollider2D             Collider;
    protected RangeAttackWeaponType     Parent;

    public virtual void Awake()
    {
        Parent = GetComponent<RangeAttackWeaponType>();
        Collider = GetComponent<BoxCollider2D>();
        Collider.enabled = false;
    }

    ///처음 한 번만 실행
    public virtual void OnEnter(Action<Entity> action, Action effect) 
    {
        HitAction = action;
        HitEffect = effect;
    }
    /// 주기적으로 실행
    public virtual void OnUpdate() { }

    /**
    * 상속받은 하위 클래스에서 조건이 만족되었을 때 호출되는 함수
    * 폭발 범위의 적에게 데미지를 주는 함수
    * 
    * @param 
    * @return null
    * @exception 
    */
    /* 구현 방법이 올바른지 모르겠음 */
    public IEnumerator OnActiveAction(Action<Entity> action, Action effect)
    {
        effect();

        Collider.enabled = true;
        TagObjects = Physics2D.OverlapBoxAll(transform.position, Parent.HitRange, Parent.Angle);

        foreach (var go in TagObjects)
        {
            if (go.TryGetComponent(out Entity entity))
            {
                ///하위 클래스에서 action형식에 대응되는 함수 작성
                /// 데미지 부여, 타입 설정 등 기능 포함
                action(entity);
            }
        }

        yield return null;
        Destroy(gameObject);
    }

}

/**
* RangeAttack을 상속받음
* 일정 시간 이후 범위 공격 형태 구현 클래스
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class RangeAttack_TYPE_TIMER : RangeAttackType
{

    public override void OnEnter(Action<Entity> action, Action effect)
    {
        base.OnEnter(action, effect);
        StartCoroutine(RangeAttack_TIMER_Cor(action, effect));
    }


    public IEnumerator RangeAttack_TIMER_Cor(Action<Entity> action, Action effect)
    {
        Parent.transform.position = Parent.TargetPos;
        yield return new WaitForSeconds(Parent.ExplosionDelay);
        StartCoroutine(OnActiveAction(action, effect));
    }
}

/**
* RangeAttack을 상속받음
* 특정 지점까지 이동(날아가) 도착 후 범위 공격을 수행하는 클래스
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class RangeAttack_TYPE_POINT : RangeAttackType
{
    private GameObject          Stage4PlayerComponent;
    private ShowWeaponPreview   Preview;

    private bool                bOnActiveActionCall;
    private Vector3             PlayerToTargetDir;

    public float               ThrowSpeed { get; set; }

    public override void Awake()
    {
        base.Awake();
        Stage4PlayerComponent = GameObject.FindWithTag("Player");
        Preview = gameObject.AddComponent<ShowWeaponPreview>();
    }

    public override void OnEnter(Action<Entity> action, Action effect)
    {
        base.OnEnter(action, effect);
        bOnActiveActionCall = false;
        Parent.transform.position = Stage4PlayerComponent.transform.position;
        PlayerToTargetDir = (Parent.TargetPos - Parent.transform.position).normalized;
        ThrowSpeed = 9.5f;

        Vector3 Loc = Parent.TargetPos;
        Preview.StartWeaponPreview(Parent.data.NoneImmediateTargetSource, Loc);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        Parent.transform.position += PlayerToTargetDir * ThrowSpeed * Time.deltaTime;
        if (Vector3.Distance(Parent.transform.position, Parent.TargetPos) < 0.2f && !bOnActiveActionCall)
        {
            bOnActiveActionCall = true;
            Preview.EndWeaponPreview();
            StartCoroutine(OnActiveAction(HitAction, HitEffect));
        }
    }
}
