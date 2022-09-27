using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
* Weapon클래스를 상속받는 무기들의 생성 및 관리를 위해 작성된 클래스
* List에 각 무기들을 등록 후, 원본 객체의 복사본 생성을 통해 발사 구현
* 오브젝트 풀 미사용
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class WeaponManager : Singleton<WeaponManager>
{
    public List<Weapon> WeaponList;
    private List<Weapon> Weapons; //내부사용 리스트
    
    private Dictionary<string, Weapon> WeaponData;
    private Vector3 UsePoint;


    private Camera mainCamera;
    private Weapon SelectedWeapon = null;
    private ShowWeaponPreview WeaponPreview;

    private void Awake()
    {
        /**
        * 리스트에 등록된 무기들을 Object Pool에 등록
        */
        mainCamera = Camera.main;
        WeaponPreview = gameObject.AddComponent<ShowWeaponPreview>();
        WeaponData = new Dictionary<string, Weapon>();
        Weapons = new List<Weapon>();
        foreach (var weapon in WeaponList)
        {
            var Go = Instantiate(weapon, transform);
            Go.name = weapon.name;
            WeaponData.Add(Go.name, Go);
            Weapons.Add(Go);
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.bLoadedScene)
        {
            return;
        }

        SetWeapon();
        //GetWeaponRemainCooltime();
        /**
        * 선택한 무기의 쿨타임 여부 및 즉시시전 여부 판단 후 기능 수행
        * 즉시시전이 아닐 경우 WeaponPreview 시작
        * 좌클릭을 통해 쿨타임 적용 및 무기 기능 수행
        */
        if (SelectedWeapon != null)
        {
            if(SelectedWeapon.data.Amount.Value > 0)
            {
                if (CooltimeQueue.Instance.CheckObjectCooltimeIsOver(SelectedWeapon.gameObject))
                {
                    if (SelectedWeapon.data.bImmediateStart == false)
                    {
                        Vector3 Loc = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        UsePoint = WeaponPreview.StartWeaponPreview(SelectedWeapon.data.NoneImmediatePreviewSource, Loc);
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        WeaponPreview.EndWeaponPreview();
                        CooltimeQueue.Instance.Add(SelectedWeapon.gameObject, SelectedWeapon.data.Cooltime.Value);
                        TryGetWeapon(SelectedWeapon);
                        SelectedWeapon.data.Amount.Value--;
                        Init();
                    }
                }
                else
                {
                    SelectedWeapon = null;
                }
            }
            else
            {
                SelectedWeapon = null;
                Debug.Log("모두 사용함.");
            }
        }

    }

    private void Init()
    {
        UsePoint = Vector3.zero;
        SelectedWeapon = null;
    }

    /*private void GetWeaponRemainCooltime()
    {
        for(int i = 0; i < WeaponData.Count; ++i)
        {
            //Debug.Log(Weapons[i].name + "remain cooldown : " + Weapons[i].data.RemainCooltime.Value);
            if (!CooltimeQueue.Instance.CheckObjectCooltimeIsOver(Weapons[i].gameObject))
            {
                Weapons[i].data.RemainCooltime.Value = CooltimeQueue.Instance.TryGetObjectRemainCooltime(Weapons[i].gameObject);
            }
        }
    }*/

    public Weapon GetWeaponInDict(string key)
    {
        WeaponData.TryGetValue(key, out var weapon);
        return weapon;
    }

    private bool RemoveAtList(Weapon weapon)
    {
        bool a = WeaponData.Remove(weapon.name);
        bool b = WeaponList.Remove(weapon);
        return a && b;
    }

    ///무기 키 입력 바인딩, ESC입력을 통해 취소
    private void SetWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Z) && WeaponData.TryGetValue("Grenade", out var v1))
        {
            SelectedWeapon = v1;
        }
        if (Input.GetKeyDown(KeyCode.X) && WeaponData.TryGetValue("FlashBang", out var v2))
        {
            SelectedWeapon = v2;
        }
        if(SelectedWeapon != null && (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)))
        {
            WeaponPreview.EndWeaponPreview();
            SelectedWeapon = null;
        }
    }


    /**
    * 선택된 무기 생성 및 기능 실행
    * 
    * @param Weapon W
    * @return 생성한 Weapon Type 반환
    * @exception 
    */
    private Weapon TryGetWeapon(Weapon W)
    {
        Weapon weapon = null;
        weapon = Instantiate(W);
        weapon.name = W.name;
        weapon.Execute(UsePoint);
        return weapon;
    }
}
