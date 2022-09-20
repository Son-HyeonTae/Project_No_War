using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* 유저가 플레이 하게 될 Player의 기능구현 cs
* 마우스 방향, 위치로 총신을 회전시키는 기능
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class PlayerControl : MonoBehaviour
{

    private Vector3 LocationSelf;
    private Vector3 MousePosition;
    private float Angle;


    /**
    * 총신 회전에 필요한 방향을 구하기 위해 Unity의 MainCamera의 위치정보를 이용
    */
    private Camera MainCamera;



    private void Awake()
    {
        MainCamera = Camera.main;
    }
    private void Update()
    {
        if (!GameManager.Instance.bLoadedScene)
        {
            Debug.Log("Don't Load Scene");
            return;
        }

        GetAxisAngle();
    }


    /**
    * 플레이어 총신 회전 구현부
    * 
    * @param 
    * @return null
    * @exception 
    */
    private void GetAxisAngle()
    {
        LocationSelf = transform.position;

        MousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        Angle = (Mathf.Atan2(MousePosition.y - LocationSelf.y, MousePosition.x - LocationSelf.x) * Mathf.Rad2Deg) - 90;
        float FixedAngle = Mathf.Clamp(Angle, -45, 44);

        transform.rotation = Quaternion.AngleAxis(FixedAngle, Vector3.forward);
    }
}
