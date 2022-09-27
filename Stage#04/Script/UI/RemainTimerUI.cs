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
            return;
        }
        string m = '0' + ((int)Flag.RemainTime / 60).ToString();
        string s = ((int)Flag.RemainTime % 60) < 10 ? '0' + ((int)Flag.RemainTime % 60).ToString() : ((int)Flag.RemainTime % 60).ToString();

        TimerText.text = m + ":" + s;
    }
}
