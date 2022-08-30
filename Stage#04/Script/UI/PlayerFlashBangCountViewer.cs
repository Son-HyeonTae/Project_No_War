using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//FlashBang
public class PlayerFlashBangCountViewer : MonoBehaviour
{
    private TextMeshProUGUI textFlashBangCount;
    public Weapon TargetWeaponPrefab;

    private void Awake()
    {
        textFlashBangCount = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        textFlashBangCount.text = WeaponManager.Instance.GetWeaponInDict(TargetWeaponPrefab.name).data.Amount.Value.ToString();
    }
}