using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
* Weapon을 상속받는 모든 객체의 공통 데이터
* ****직접 참조를 통한 값 변경X****
* ScriptableObject.CreateInstance<>();키워드를 통해 인스턴싱 후 사용
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Object Asset/WeaponData")]
public class WeaponData : ScriptableObject
{
    public DATA<float> Damage = new DATA<float>();
    public DATA<float> Cooltime = new DATA<float>();
    public DATA<int> Amount = new DATA<int>();

    public bool bImmediateStart;                                ///즉시 시전 플래그, 활성화 시, WeaponPreview를 수행하지 않음
    public GameObject NoneImmediatePreviewSource;               ///지점조작 WeaponPreview Sprite
    public GameObject NoneImmediateTargetSource;                ///투사체 도착 지점 표시 WeaponPreview Sprite 
}


/**
* 무기 최상위 클래스
* 이 클래스를 상속받아 하위 Weapon클래스 구현
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class Weapon : MonoBehaviour 
{
    [HideInInspector] public WeaponData data { get; protected set; }
    [SerializeField] private GameObject NoneImmediatePreviewSource;
    [SerializeField] private GameObject NoneImmediateTargetSource;
    
    public virtual void Init()
    {
        data = ScriptableObject.CreateInstance<WeaponData>();
        data.Damage.Value = 0;
        data.Cooltime.Value = 0;
        data.Amount.Value = 9999;
        data.bImmediateStart = false;
        data.NoneImmediatePreviewSource = this.NoneImmediatePreviewSource;
        data.NoneImmediateTargetSource = this.NoneImmediateTargetSource;
    }
    public virtual void Execute(Vector3 coordinate) { }
    public virtual void Execute() { }
}
