using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCameraMovement : MonoBehaviour
{
    private Vector3 CharacterCameraRotionVector;
    public float VerticalSensitivity = 1.0f;
    public float HorizontalSensitivity = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotate();
    }

    void CameraRotate()
    {
        CharacterCameraRotionVector.y += Input.GetAxis("Mouse X") * HorizontalSensitivity * Time.deltaTime;
        CharacterCameraRotionVector.x += -Input.GetAxis("Mouse Y") * VerticalSensitivity * Time.deltaTime;

        CharacterCameraRotionVector.z = 0.0f;
        CharacterCameraRotionVector.x = Mathf.Clamp(CharacterCameraRotionVector.x, -50, 70);

        transform.rotation = Quaternion.Euler(CharacterCameraRotionVector);
        
    }

    public Vector3 GetCharacterCameraRotationVector()
    {
        return CharacterCameraRotionVector;
    }
}
