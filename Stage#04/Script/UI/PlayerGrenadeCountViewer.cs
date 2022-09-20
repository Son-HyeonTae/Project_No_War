using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerGrenadeCountViewer : MonoBehaviour
{
    private TextMeshProUGUI textMineCount;
    public Weapon TargetWeaponPrefab;

    private void Awake()
    {
        textMineCount = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!GameManager.Instance.bLoadedScene)
        {
            Debug.Log("Don't Load Scene");
            return;
        }
        textMineCount.text = WeaponManager.Instance.GetWeaponInDict(TargetWeaponPrefab.name).data.Amount.Value.ToString();
    }
}
