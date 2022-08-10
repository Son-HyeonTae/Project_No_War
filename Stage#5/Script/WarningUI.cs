using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningUI : MonoBehaviour
{
    // line renderer로 경고 라인 그리기.
    public LineRenderer myLenderer;
    void Start()
    {
        myLenderer = GetComponent<LineRenderer>();

        Color nowStartColor = myLenderer.startColor;
        Color nowEndColor = myLenderer.endColor;

        myLenderer.startColor = new Color(1f, 0f, 0f, 0.38f);
        myLenderer.endColor = new Color(1f, 0.7f, 0.7f, 0.38f);


        // myLenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0.3f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
