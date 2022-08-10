using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mob
{
    private ObjectPoolRegisterData RgsData;
    private EnemyStateMachine StateMachine;
    private float ElapsedTime;

    private Statement_Move MoveState;
    private Statement_Stiff StiffState;
    private Statement_Hide HideState;

    public override void Awake()
    {
        base.Awake();
        RgsData = new ObjectPoolRegisterData();
        ElapsedTime = 0.0f;
        StateInit();
    }

    private void StateInit()
    {
        StateMachine = gameObject.AddComponent<EnemyStateMachine>();
        MoveState = gameObject.AddComponent<Statement_Move>();
        StiffState = gameObject.AddComponent<Statement_Stiff>();
        HideState = gameObject.AddComponent<Statement_Hide>();
        StateMachine.Initialize(MoveState);
    }

    private void Update()
    {
        if(data.bDead)
        {
            StartCoroutine(RequestDespawn());
        }
    }

    IEnumerator RequestDespawn()
    {
        RgsData.Prefab = gameObject;
        RgsData.Key = name;
        ObjectPoolManager.Instance.Despawn(RgsData);
        yield return null;
    }
}

public abstract class Statement : MonoBehaviour
{
    public abstract void Execute(Entity CallObject);
    public virtual void OnEnter() { }

    public virtual void OnExit() { }
    public bool bCanChange = true;
}


public class Statement_Move : Statement
{
    Entity Parent;

    //--AStar
    private Vector3 TraceTarget;
    private Vector3[] path;
    private int targetIndex;
    private bool bPathFinding;

    public void OnDisable()
    {
        bPathFinding = false;
        targetIndex = 0;
        path = null;
    }

    public override void Execute(Entity CallObject)
    {
        Parent = CallObject;
        if(!bPathFinding)
        {
            PathFind();   
        }

    }

    public override void OnEnter()
    {
        bCanChange = true;
        bPathFinding = false;
        targetIndex = 0;
        path = null;
    }

    public override void OnExit()
    {
        StopCoroutine(FollowPath());
        bPathFinding = false;
        targetIndex = 0;
        path = null;
    }

    private void PathFind()
    {
        TraceTarget = new Vector3(Random.Range(-3.0f, 3.0f), -5);
        PathRequestManager.Instance.RequestPath(Parent.transform.position, TraceTarget, OnPathFound);
        bPathFinding = true;
    }

    public void OnPathFound(Vector3[] Waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = Waypoints;
            StopCoroutine(FollowPath());
            StartCoroutine(FollowPath());
        }
        else
        {
            path = null;
        }
    }

    //Move To Path
    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, Parent.data.Velocity.Value * Time.deltaTime);
            yield return null;
        }
    }
}
public class Statement_Stiff : Statement
{
    private float TotalStiffTime;
    private float ElapsedTime;
    private bool bStiff;

    public override void Execute(Entity CallObject)
    {
        if(bStiff)
        {
            bCanChange = false;
            ElapsedTime += Time.deltaTime;

            if(ElapsedTime >= TotalStiffTime)
            {
                bStiff = false;
                bCanChange = true;
            }
            //Animation Delegate
        }
    }

    public void SetTime(float time)
    {
        TotalStiffTime = time;
    }

    public override void OnEnter()
    {
        bStiff = false;
        ElapsedTime = 0.0f;
    }

    public override void OnExit()
    {
        ElapsedTime = 0.0f;
        TotalStiffTime = 0;
        bStiff = false;
    }
}
public class Statement_Hide : Statement
{
    private GameObject target;
    private Collider2D HideBases;

    public override void OnEnter()
    {
        bCanChange = false;
    }

    public override void OnExit()
    {
    }

    public override void Execute(Entity CallObject)
    {
        
        if(FindHidebase())
        {
            Debug.Log("Found base");

            Vector3.MoveTowards(transform.position, HideBases.transform.position, CallObject.data.Velocity.Value * Time.deltaTime);
            if (Vector3.Distance(CallObject.transform.position, target.transform.position) < 0.1f)
            {
                StartCoroutine(WaitFor(2.0f));
            }
        }
        bCanChange = true;
    }

    private bool FindHidebase()
    {
        var Hide = Physics2D.OverlapBox(transform.position, Vector3.one * 5, 0);
        if (Hide.CompareTag("HideObject"))
        {
            HideBases = Hide;
            return true;
        }
        return false;
    }

    private IEnumerator WaitFor(float t)
    {
        yield return new WaitForSeconds(t);
        bCanChange = true;
    }
}