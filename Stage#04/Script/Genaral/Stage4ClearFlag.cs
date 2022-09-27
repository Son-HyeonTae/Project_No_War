using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* 스테이지4 클리어 및 실패 조건을 기술한 클래스
* 남은시간 관리
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class Stage4ClearFlag : Singleton<Stage4ClearFlag>
{
    public enum Stage4GameFlag
    {
        PLAYING,
        CLEAR,
        FAIL
    }

    private float ClearTime;
    float ElapsedTime = 0;
    public float RemainTime { get; private set; } = 0.0f;
    public Stage4GameFlag Stage4Flag { get; private set; }
    public bool bStage4End { get; private set; }

    private void Awake()
    {
        ClearTime = 120.0f;
        Stage4Flag = Stage4GameFlag.PLAYING;
        bStage4End = false;
    }

    private void Update()
    {
        /**
        * 
        * 
        */
        if(!GameManager.Instance.bLoadedScene)
        {
            return;
        }



        Transition();
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Stage4Flag = Stage4GameFlag.FAIL;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Stage4Flag = Stage4GameFlag.CLEAR;
        }

        GameManager.Instance.LoadStage(
            () => { return Stage4Flag == Stage4GameFlag.CLEAR; },
            
            GameManager.Instance.CutScene09
            );

        GameManager.Instance.LoadStage(
            () => { return Stage4Flag == Stage4GameFlag.FAIL; },
            GameManager.Instance.CutScene09
            );
    }

    private void Transition()
    {
        ElapsedTime += Time.deltaTime;
        RemainTime = ClearTime - ElapsedTime;
        Debug.Log(RemainTime);
        if (RemainTime <= 0.0f)
        {
            RemainTime = 0.0f;
            ElapsedTime = 0.0f;
            Stage4Flag = Stage4GameFlag.CLEAR;
            bStage4End = true;


            ObjectPoolStorage.Instance.Pool_Enemy.DestroyAllObject();
            ObjectPoolStorage.Instance.Pool_Projectile.DestroyAllObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Stage4Flag = Stage4GameFlag.FAIL;
            bStage4End = true;

            ObjectPoolStorage.Instance.Pool_Enemy.DestroyAllObject();
            ObjectPoolStorage.Instance.Pool_Projectile.DestroyAllObject();
        }
    }
}
