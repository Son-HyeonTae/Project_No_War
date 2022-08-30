using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
* Entity를 상속받는 모든 객체의 공통 데이터
* ****직접 참조를 통한 값 변경X****
* ScriptableObject.CreateInstance<>();키워드를 통해 인스턴싱 후 사용
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Object Asset/EntityData")]
public class EntityData : ScriptableObject
{
    public DATA<float> baseHP   = new DATA<float>();
    public DATA<float> HP       = new DATA<float>();
    public DATA<float> Velocity = new DATA<float>();

    public bool bDead { get; set; }
}

