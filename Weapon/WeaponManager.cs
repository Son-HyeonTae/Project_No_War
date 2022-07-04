using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KeyType = System.String;

/// <summary>
/// 원본 프리팹을 Dictionary에 저장 후
/// 키를 누르면 프리팹 복사본 생성
/// 쿨타임은 복사본에서 스킬을 확정적으로 사용하면
/// 복사본에서 원본 bReady를 변경
/// 
/// 쿨타임은 WeaponManager에서만 작동
/// </summary>

public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField] public List<Weapon> Weapons = new List<Weapon>();
    private List<Weapon> WeaponCopy = new List<Weapon>();
    private Dictionary<KeyType, Weapon> WeaponDict = new Dictionary<KeyType, Weapon>(); //원본 Prefab dict
    
    [HideInInspector] public Weapon CurrentWeapon;
    [HideInInspector] public Weapon PrevWeaponData;

    private Weapon obj;


    private void Start()
    {
        for (int i = 0; i < Weapons.Count; i++)
        {
            Weapons[i].bReady = true;
            WeaponDict.Add(Weapons[i].name, Weapons[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetWeapon();
        if (CurrentWeapon != null && obj == null)
        {
            if (CurrentWeapon.bReady)
            {
                obj = Instantiate(CurrentWeapon);
                obj.name = CurrentWeapon.name;
                WeaponCopy.Add(obj);
            }
            else
            {
                CurrentWeapon = null;
            }
        }
        if (obj != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Cooltime(CurrentWeapon.Cooltime, CurrentWeapon));
                obj = null;
                PrevWeaponData = CurrentWeapon;
                CurrentWeapon = null;
            }
            if(Input.GetMouseButtonDown(1))
            {
                obj = null;
                PrevWeaponData = CurrentWeapon;
                CurrentWeapon = null;
            }
        }



        for (int i = 0; i < WeaponCopy.Count; i++)
        {
            WeaponCopy[i].Fire();
        }
    }

    void GetWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PrevWeaponData = CurrentWeapon;
            WeaponDict.TryGetValue("Mine", out CurrentWeapon);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            PrevWeaponData = CurrentWeapon;
            WeaponDict.TryGetValue("Grenade", out CurrentWeapon);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CurrentWeapon = null;
        }
    }

    IEnumerator Cooltime(float t, Weapon type)
    {
        type.Amount -= 1;
        Debug.Log(type.Amount + type.name);
        type.bReady = false;
        yield return new WaitForSeconds(t);
        type.bReady = true;
    }

    public void RemoveAtCopyList(Weapon W)
    {
        WeaponCopy.Remove(W);
    }
}
