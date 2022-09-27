using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerGrenadeCountViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textFlashBangCount;
    private Weapon TargetWeaponPrefab;

    private void Start()
    {
        TargetWeaponPrefab = GameObject.Find("Grenade").GetComponent<Weapon>();

    }

    private void Update()
    {
        Weapon data = WeaponManager.Instance.GetWeaponInDict(TargetWeaponPrefab.name);
        if (data)
            textFlashBangCount.text = data.data.Amount.Value.ToString();
    }
}
