using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHpUI : MonoBehaviour
{
    public EntityData data; //scriptable class
    public Image HPImage;
    public Image Background;
    private float HPUIData;
    private Mob AttachmentObject;
    private RectTransform MyRT;
    private Vector3 AttachOffset;

    private Canvas canvas;
    private Camera MainCamera;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        MainCamera = Camera.main;
        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        MyRT = GetComponent<RectTransform>();

        AttachOffset = Vector3.zero;
        AttachmentObject = null;
        HPUIData = data.HP.Value;
    }

    private void Update()
    {
        if (AttachmentObject)
        {
            MyRT.SetParent(canvas.transform);
            HPImage.enabled = (!AttachmentObject.data.bDead);
            Background.enabled = (!AttachmentObject.data.bDead);
            Vector3 ConvPos = MainCamera.WorldToScreenPoint(AttachmentObject.transform.position + AttachOffset);
            MyRT.position = ConvPos;


            if (HPUIData != data.HP.Value && data)
            {
                StartCoroutine(CalculateHp());
                HPUIData = data.HP.Value;
            }
        }
        if(AttachmentObject.data.bDead)
        {
            HPImage.fillAmount = 1;
        }
    }

    IEnumerator CalculateHp()
    {
        HPImage.fillAmount = (float)data.HP.Value / (float)data.baseHP.Value;

        yield return null;
    }

    public void Attachment(Mob target, Vector3 Offset, EntityData entityData)
    {
        data = entityData;
        AttachmentObject = target;
        AttachOffset = Offset;
    }
}
