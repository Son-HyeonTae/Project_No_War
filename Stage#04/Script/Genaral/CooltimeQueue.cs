using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
* GameObject Type 객체의 쿨타임을 설정 및 관리하는 클래스
* Item, Skill의 쿨타임
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class CooltimeQueue : Singleton<CooltimeQueue>
{

    ///=============================================
    ///      primitive  
    ///=============================================
    private Dictionary<string, CooltimeQueueRegisterData> OriginalObjectDict;

    ///=============================================
    ///      private struct
    ///=============================================

    /*
    * 쿨타임 적용을 위해 필요한 정보 구조체
    */
    private struct CooltimeQueueRegisterData
    {
        public GameObject   Object;                           //쿨타임이 적용될 객체의 원본
        public float        BeginTime;                             
        public float        ObjectCooltime;
        public float        RemainCooltime;
    }



    ///=============================================
    ///      private method etc
    ///=============================================

    private void Awake()
    {
        OriginalObjectDict = new Dictionary<string, CooltimeQueueRegisterData>();
    }


    ///=============================================
    ///      Cooldown Func
    ///=============================================
    /**
    * 매개변수로 받은 Object의 쿨타임이 끝났는지 확인하는 함수
    * 
    * @ param       GameObject Object - 확인할 객체의 reference
    * @ return      래퍼런스 객체의 쿨타임이 끝났다면 true 다른경우 false 반환
    * @ exception 
    */
    public bool CheckObjectCooltimeIsOver(GameObject Object)
    {
        if (OriginalObjectDict.TryGetValue(Object.name, out var ago))
        {
            Debug.Log(Object.GetType().Name + "Cooltime");
            return false;
        }
        return true;
    }

    public float TryGetObjectRemainCooltime(GameObject Object)
    {
        if(OriginalObjectDict.TryGetValue(Object.name, out var val))
        {
            return val.RemainCooltime;
        }
        return -1;
    }


    /**
    * 쿨타임 목록에 추가
    * 
    * @ param       GameObject O - 추가할 객체의 reference
    * @ param       float time - 적용할 쿨타임 수치
    * @ return      이미 쿨타임이 적용중이라면 false 이외 true
    * @ exception   이미 쿨타임이 적용중이라면 추가되지 않음
    */
    public bool Add(GameObject O, float time)
    {
        if (OriginalObjectDict.TryGetValue(O.name, out var go))
        {
            return false;
        }
        CooltimeQueueRegisterData pair = new CooltimeQueueRegisterData();
        pair.Object = O;
        pair.BeginTime = GameManager.Instance.GameTime;
        pair.ObjectCooltime = time;
        OriginalObjectDict.Add(O.name, pair);
        StartCoroutine(AddCooldownCor(pair));
        return true;
    }

    /**
    * Add() 함수에서 호출됨, 실체 쿨타임 수치 적용 및 계산
    * Add() 함수에서 작성된 CooltimeQueueRegisterData 구조체를 바탕으로 계산
    * 
    * @ param       CooltimeQueueRegisterData data data - 등록 정보를 담고있는 구조체 양식
    * @ return      Iterator형식, 반환 없음
    * @ exception   data의 모든 요소의 작성 여부
    */
    private IEnumerator AddCooldownCor(CooltimeQueueRegisterData data)
    {
        if(OriginalObjectDict.TryGetValue(data.Object.name, out var go))
        {
            go.RemainCooltime = go.ObjectCooltime - (GameManager.Instance.GameTime - go.BeginTime);
            yield return new WaitForSeconds(go.ObjectCooltime);
            OriginalObjectDict.Remove(go.Object.name);
        }
        yield return null;
    }

    //---------------------------------------------------------------

    /**
    * 짧은 Delay발생과 같은 상황에서 사용하기 위해 작성
    * 
    * @ param       float delay - delay
    * @ param       Action DelayChecker - Callback형식의 Param 호출 스크립트에서 flag를 변환하는 함수 작성 후 사용
    * @ return      Iterator형식, 반환 없음
    * @ exception   
    */
    public IEnumerator ShortDelayChecker(float delay, Action DelayChecker)
    {
        yield return new WaitForSeconds(delay);
        DelayChecker();
    }
}
