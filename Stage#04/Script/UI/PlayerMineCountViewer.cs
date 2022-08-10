using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//FlashBang
public class PlayerMineCountViewer : MonoBehaviour
{
    private TextMeshProUGUI textMineCount;

    private void Awake()
    {
        textMineCount = GetComponent<TextMeshProUGUI>();
    }
}