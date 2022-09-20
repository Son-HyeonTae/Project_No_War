using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RemainTimerUI : MonoBehaviour
{
    public Stage4ClearFlag Flag;
    public Image Icon;
    private TextMeshProUGUI TimerText;

    private void Awake()
    {
        TimerText = GetComponent<TextMeshProUGUI>();
    }

    /*private void Start()
    {
        StartCoroutine(Blink());
    }*/
    private void Update()
    {
        if (!GameManager.Instance.bLoadedScene)
        {
            Debug.Log("Don't Load Scene");
            return;
        }
        string m = '0' + (Flag.RemainTime / 60).ToString("F0");
        string s = (Flag.RemainTime % 60) < 10 ? '0' + (Flag.RemainTime % 60).ToString("F0") : (Flag.RemainTime % 60).ToString("F0");

        TimerText.text = m + ":" + s;
    }
}
