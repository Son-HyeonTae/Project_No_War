using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWeaponPreview : MonoBehaviour
{
    private GameObject ShowObject;
    private Camera mainCamera;

    public void Init()
    {
        mainCamera = Camera.main;
    }

    public Vector3 StartWeaponPreview(GameObject Source)
    {
        if(Source != null && ShowObject == null)
            ShowObject = Instantiate(Source);

        Vector3 Go = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Go.z = 0;
        ShowObject.transform.position = Go;
        return ShowObject.transform.position;
    }

    public void EndWeaponPreview()
    {
        Destroy(ShowObject);
    }
}
