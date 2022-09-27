using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//FlashBang
public class PlayerFlashBangCountViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textFlashBangCount;
    private Weapon TargetWeaponPrefab;

    private void Start()
    {
        TargetWeaponPrefab = GameObject.Find("FlashBang").GetComponent<Weapon>();
        
    }

    private void Update()
    {
        Weapon data = WeaponManager.Instance.GetWeaponInDict(TargetWeaponPrefab.name);
        if (data)
            textFlashBangCount.text = data.data.Amount.Value.ToString();
    }
}