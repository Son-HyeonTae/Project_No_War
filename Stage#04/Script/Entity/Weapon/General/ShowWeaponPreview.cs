using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
* 스킬, 무기의 시전 위치 표시를 위해 작성된 클래스
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class ShowWeaponPreview : MonoBehaviour
{
    private GameObject ShowObject;                          ///시전 위치 표시 아이콘


    /**
    * 위치 표시 구현부
    * 
    * @param GameObject Source - 표시할 아이콘 sprite
    * @param Vector3 Location - 아이콘을 표시할 위치
    * @return 표시된 위치를 반환 
    * @exception 현재 표시되고있는 sprite와 매개변수의 sprite가 달라야 함
    */
    public Vector3 StartWeaponPreview(GameObject Source, Vector3 Location)
    {
        if(Source != ShowObject)
        {
            if (ShowObject)
                EndWeaponPreview();
            ShowObject = Instantiate(Source);
        }

        Vector3 Go = Location;
        Go.z = 0;
        ShowObject.transform.position = Go;
        return ShowObject.transform.position;
    }

    public void EndWeaponPreview()
    {
        Destroy(ShowObject);
    }
}
