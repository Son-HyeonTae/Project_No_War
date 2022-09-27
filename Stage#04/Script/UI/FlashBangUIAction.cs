using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashBangUIAction : MonoBehaviour
{
    Image FlashImage;

    private void Awake()
    {
        FlashImage = GetComponent<Image>(); 
    }

    private void Start()
    {
        FlashImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    public void Flash()
    {
        StartCoroutine(GameManager.Instance.FadeIn(0.6f, 0.0f, FlashImage));

    }
}
