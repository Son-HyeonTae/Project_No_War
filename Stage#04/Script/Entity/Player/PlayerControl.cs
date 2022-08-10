using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMOVE_TYPE
{
    FIXED,
    MOVEMENT
}

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;

    private Vector2 Target, Mouse;
    private float Angle;

//--
    [SerializeField] private eMOVE_TYPE MoveType;


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetMouseHorizontalAxis());
        
        switch(MoveType)
        {
            case eMOVE_TYPE.FIXED:
                MoveTypeFixed();
                break;
            case eMOVE_TYPE.MOVEMENT:
                MoveTypeMovement();
                break;
        }

    }




    void MoveTypeFixed()
    {
        Target = transform.position;

        Mouse = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        Angle = Mathf.Atan2(Mouse.y - Target.y, Mouse.x - Target.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.AngleAxis(Angle - 90, Vector3.forward);
    }
    void MoveTypeMovement()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        float MousePositionX = MainCamera.ScreenToWorldPoint(Input.mousePosition).x;

        transform.position = new Vector3(MousePositionX, transform.position.y);
    }
}
