using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Enemy의 Animation제어를 위해 작성된 클래스
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/

public class EnemyAnimController : MonoBehaviour
{
    /* Animation의 타입과 이름이 저장될 Dict */
    private Dictionary<string, eAnimType> AnimationDataDict;
    public enum eAnimType{ INT, BOOL, TRIGGER }

    ///======================================
    ///     Animation Statement
    ///     VarName + TR ** Trigger 속성을 포함
    ///======================================
    private string IDLE      { get; } = "Idle";
    private string IsWalk    { get; } = "Walk";
    private string IsHide    { get; } = "Hide";
    private string IsFaintTR { get; } = "Faint";
    private string IsStiffTR { get; } = "Stiff";

    ///======================================
    ///Animation Control Variables
    ///======================================
    private const string    InitialAnimation = "Anim";
    public string           CurrentAnimation { get; private set; }
    public string           PreviousAnimation { get; private set; }

    ///======================================
    ///      Reference Components
    ///======================================
    private Animator        AnimComponent;
    private Enemy           Self;
    private SpriteRenderer  Renderer;
    //private StateMachine StateMachineSelf;

    ///======================================
    ///     Animation Flip Variable
    ///======================================
    private Vector3 PreviousPositionSelf;
         

    void Awake()
    {
        Self            = GetComponent<Enemy>();
        AnimComponent   = GetComponent<Animator>();
        Renderer        = GetComponent<SpriteRenderer>();

        ///상태가 추가될 때 마다 등록해야 함
        AnimationDataDict = new Dictionary<string, eAnimType>();
        AnimationDataDict.Add(IsWalk, eAnimType.BOOL);
        AnimationDataDict.Add(IsHide, eAnimType.BOOL);
        AnimationDataDict.Add(IsFaintTR, eAnimType.BOOL);
        AnimationDataDict.Add(IsStiffTR, eAnimType.BOOL);

        //StateMachineSelf = Self.StateMachine;
        CurrentAnimation    = InitialAnimation;
        PreviousAnimation   = CurrentAnimation;


        PreviousPositionSelf = transform.position;
    }

    // Update is called once per frame
    /*void Update()
    {
        AnimationAutoFlip();
    }*/
    /**
    * 애니메이션 좌우 반전 구현부
    * 플레이어의 이동 방향에 따라 결정
    * 
    * @not use
    * @param 
    * @return null
    * @exception 
    */
    private void AnimationAutoFlip()
    {
        Renderer.flipX = (transform.position.x > PreviousPositionSelf.x) ? false : true;
        PreviousPositionSelf = transform.position;
    }



    /**
    * 애니메이션 트랜지션 구현부 
    * 직전 실행 애니메이션 정지 -> 현재 애니메이션 실행
    * @param string Name - 전환할 애니메이션 이름
    * @param string int - Bool, Trigger타입일 경우 사용하지 않음, Int형식의 애니메이션에 사용
    * @return null
    * @exception 
    */
    public void SwitchAnimation(string Name, int Value = 0)
    {
        if(AnimationDataDict.TryGetValue(Name, out eAnimType CurrentAnimType))
        {
            if (CurrentAnimation == Name) return;
            //Break Previous Animation Statement
            if (AnimationDataDict.TryGetValue(CurrentAnimation, out eAnimType PrevAnimType))
            {
                if(PrevAnimType == eAnimType.BOOL)
                    AnimComponent.SetBool(CurrentAnimation, false);
            }

            PreviousAnimation = CurrentAnimation;
            CurrentAnimation = Name;
            //Debug.Log("Switched " + PreviousAnimation + " -> " + CurrentAnimation);
        }

        //Switch Current Animation Statement 
        switch (CurrentAnimType)
        {
            case eAnimType.INT:
                AnimComponent.SetInteger(CurrentAnimation, Value);
                break;
            case eAnimType.BOOL:
                AnimComponent.SetBool(CurrentAnimation, true);
                break;
            case eAnimType.TRIGGER:
                AnimComponent.SetTrigger(CurrentAnimation);
                break;
            default:
                break;
        }
    }

}
