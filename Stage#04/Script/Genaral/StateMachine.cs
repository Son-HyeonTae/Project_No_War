using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
* Enemy State 관리를 위해 작성된 클래스
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class StateMachine : MonoBehaviour
{
    ///====================================
    ///     Statement
    ///====================================
    [HideInInspector] private Statement CurrentState;
    [HideInInspector] private Statement PreviousState;
    public Statement        GetCurrentState         { get { return CurrentState; } }
    public Statement        PreviousPreviousState   { get { return PreviousState; } }
    public Statement_Move   MoveState               { get; private set; }
    public Statement_Hide   HideState               { get; private set; }
    public Statement_Stiff  StiffState              { get; private set; }
    public Statement_IDLE   IDLEState               { get; private set; }
    public Statement_Faint  FaintState              { get; private set; }


    private Entity EntityComponent;

    private void Awake()
    {
        EntityComponent = GetComponent<Entity>();
        MoveState           = gameObject.AddComponent<Statement_Move>();
        HideState           = gameObject.AddComponent<Statement_Hide>();
        StiffState          = gameObject.AddComponent<Statement_Stiff>();
        IDLEState           = gameObject.AddComponent<Statement_IDLE>();
        FaintState          = gameObject.AddComponent<Statement_Faint>();

    // State Parent Initialize Point -- StateMachine.Awake()
        MoveState.Parent    = EntityComponent;
        StiffState.Parent   = EntityComponent;
        HideState.Parent    = EntityComponent;
        IDLEState.Parent    = EntityComponent;
        FaintState.Parent   = EntityComponent;
    }

    public bool Initialize(Statement state)
    {
        if (state == null)
            return false;

        CurrentState = state;
        CurrentState.OnEnter();
        Debug.Log("Started Statemachine " + CurrentState);
        return CurrentState != null;
    }

    //-- OldCurrentState.Exit()
    //          V
    //--NewCurrentState = IN
    //          V
    //PrevState = OldCurrentState
    //          V
    //NewCurrentState.Enter()
    public bool Change(Statement state)
    {
        if (CurrentState != null && CurrentState.bCanChange && state.bCanChange)
        {
            if (CurrentState == state)
                return false;

            CurrentState.OnExit();
            PreviousState = CurrentState;
            CurrentState = state;
            CurrentState.OnEnter();

            Debug.Log("Changed " + PreviousState + " -> " + CurrentState);
            return true;
        }
        else
        {
            Initialize(state);
        }
        return false;
    }

    private void Update()
    {
        //Debug.Log(CurrentState);
        if (CurrentState != null)
        {
            CurrentState.Execute();
            CurrentState.Condition();
        }
        else
        {

        }
    }
}


///==============================================================================================
/**
* Enemy의 상태 정의를 위한 기본 형식
* 모든 State는 Statement클래스를 상속받아 사용하게 됨
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public abstract class Statement : MonoBehaviour
{
    ///현재 상태 유지중 반복하여 실행
    public virtual void Execute() { }

    ///처음 상태가 전이되었을 때 한 번 실행
    public virtual void OnEnter()
    {
        Machine = Parent.StateMachine;
    }

    ///다른 상태로 전이되어지기 전 한 번 실행
    public virtual void OnExit() { }
    
    ///다른 상태로 전이되어지기 위한 조건을 기술하는 함수, Execute와 같이 반복하여 실행
    public virtual void Condition() { }

    ///현재 상태 유지 중, 다른상태로 전이 가능한 상태인지 확인하는 변수
    public bool bCanChange = true;
    public Entity Parent;
    protected StateMachine Machine;
}

/**
* Enemy의 기절 상태 구현
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class Statement_Faint : Statement
{
    public float Duration { get; set; }
    private float ElapsedTime;
    private static bool bEndFaint;

    private void Awake()
    {
        bEndFaint = true;
        Duration = 0.0f;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        if (!bEndFaint)
            return;

        Parent.AnimController.SwitchAnimation("Faint");

        if (Duration == 0.0f) Duration = 1.5f;
        bEndFaint = true;
        bCanChange = false;
        ElapsedTime = 0;
    }

    public override void Condition()
    {
        base.Condition();
        bEndFaint = false;

        ElapsedTime += Time.deltaTime;
        if (ElapsedTime >= Duration)
        {
            bCanChange = true;
            bEndFaint = true;
            //Parent.AnimController.SwitchAnimation("Walk");
            Machine.Change(Machine.MoveState);
        }
    }
}

/**
* Enemy의 기절 상태 구현
* 경직 상태보다 지속시간이 긺
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class Statement_Stiff : Statement
{
    public float Duration { get; set; }
    public Vector3 HitDir { get; set; }

    private float ElapsedTime;
    private static bool bStiffEnd;


    private void Awake()
    {
        bStiffEnd = true;
        Duration = 0.0f;
        HitDir = Vector3.zero;
        ElapsedTime = 0.0f;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (!bStiffEnd)
            return;

        Parent.AnimController.SwitchAnimation("Stiff");

        if (Duration == 0.0f) Duration = 0.3f;
        if (HitDir == Vector3.zero) HitDir = transform.up;

        ElapsedTime = 0.0f;
        bCanChange = false;
        bStiffEnd = true;
    }

    public override void Condition()
    {
        base.Condition();
        bStiffEnd = false;

        ElapsedTime += Time.deltaTime;
        if (ElapsedTime >= Duration)
        {
            bCanChange = true;
            bStiffEnd = true;
            Parent.AnimController.SwitchAnimation("Walk");
            Machine.Change(Machine.MoveState);
        }
    }

}

/**
* Enemy의 경직 상태 구현
* 기절보다 지속시간이 짧음
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class Statement_IDLE : Statement
{
    public override void Execute()
    {

    }
}

/**
* Enemy의 기본 상태 구현
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class Statement_Move : Statement
{
    private Vector3 BeginPoint;
    private Vector3 EndPoint;
    private List<Collider2D> OldCol;

    private void OnDisable()
    {
        Parent.PathFindSystem.StopPathFinding();
    }

    private void Awake()
    {
        OldCol = new List<Collider2D>();
    }

    public override void OnEnter()
    {
        base.OnEnter();

        BeginPoint = transform.position;
        EndPoint = new Vector3(UnityEngine.Random.Range(-3, 3), -4.5f);
        Parent.PathFindSystem.StartPathFind(BeginPoint, EndPoint);
        Parent.AnimController.SwitchAnimation("Walk");
    }

    public override void Condition()
    {

        var res = TryGetHidableObjectInRange();
        //Debug.Log(Parent.PathFindSystem.bEndPathFinding);
        if (Parent.PathFindSystem.bEndPathFinding)
        {
            Machine.Change(Machine.IDLEState);
        }
        else if (res)
        {
            Machine.HideState.HideBases = res;
            Machine.Change(Machine.HideState);
        }
    }
    public override void OnExit()
    {
        Parent.PathFindSystem.StopPathFinding();
    }

    private Collider2D TryGetHidableObjectInRange()
    {
        Collider2D[] GetObject = Physics2D.OverlapBoxAll(transform.position, Vector3.one * 2, 0);

        if (GetObject.Length > 0)
        {
            foreach (Collider2D obj in GetObject)
            {
                obj.TryGetComponent<HidableObject>(out var hidable);
                if (hidable && !hidable.bUse && !OldCol.Contains(obj))
                {
                    OldCol.Add(obj);
                    return obj;
                }
            }
        }

        return null;
    }
}

/**
* Enemy의 엄폐물 엄폐 상태 구현
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class Statement_Hide : Statement
{
    private const int InitialHideCount = 1;

    public Collider2D HideBases;
    private List<Collider2D> HidedColliders;
    private Vector3 BeginPoint, EndPoint;
    private int HideCount;

    private void OnDisable()
    {
        HidedColliders.Clear();
    }

    private void Awake()
    {
        HidedColliders = new List<Collider2D>();
        HideCount = InitialHideCount;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Parent.AnimController.SwitchAnimation("Walk");

        if (HideCount > 0 && RandomValue.Instance.Random(80))
        {
            HideCount--;
            BeginPoint = transform.position;
            EndPoint = HideBases.transform.position + new Vector3(0, HideBases.transform.localScale.y * 0.8f);
            Parent.PathFindSystem.StartPathFind(BeginPoint, EndPoint);
        }
        else
        {
            Machine.Change(Machine.MoveState);
        }
    }

    public override void OnExit()
    {
        HideCount = InitialHideCount;
    }

    public override void Execute()
    {
        if (Parent.PathFindSystem.bEndPathFinding)
        {
            StartCoroutine(WaitFor(2));
            Parent.AnimController.SwitchAnimation("Hide");
        }
    }
    private IEnumerator WaitFor(float t)
    {
        yield return new WaitForSeconds(t);
        HidedColliders.Add(HideBases);
        bCanChange = true;
        HideBases = null;
        Machine.Change(Machine.MoveState);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(EndPoint, new Vector3(1, 1));
    }
}
