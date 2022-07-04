using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float BaseVelocity = 1.0f;
    public float Velocity = 1.0f;
    public float AcceleVelocity = 1.0f;
    public float RotationSpeed = 1.0f;
    public bool IsRunning = false;
    public bool IsWalking = false;

    public const int BaseJumpCount = 2;
    public int JumpCount = 2;
    public float BaseJumpForce = 1.0f;
    public float JumpForce = 1.0f;
    public float JumpForceReductionRate = 0.0f;
    public float JumpHeight = 0.0f;

    public bool bCheckCharacterOntheGround = false;

    Rigidbody rb;
    public CharacterCameraMovement CameraMovement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CharacterRotate();
        CharacterJump();
    }

    void Movement()
    {
        /*if(Input.GetKey(KeyCode.LeftShift))
        {
            IsRunning = true;
            IsWalking = false;
            Velocity = AcceleVelocity;
        }
        else
        {
            IsRunning = false;
            IsWalking = true;
            Velocity = BaseVelocity;
        }*/
        transform.Translate(GetCharacterAxis() * Velocity * Time.deltaTime);
    }

    void CharacterJump()
    {
        //점프 최대높이 저장
         if(CharacterRayCastInfo(-transform.up, this.gameObject, float.MaxValue).distance > JumpHeight)
        {
            JumpHeight = CharacterRayCastInfo(-transform.up, this.gameObject, float.MaxValue).distance; 
        }

        if (Input.GetKeyDown(KeyCode.Space) && JumpCount > 0)
        {
            rb.AddForce(transform.up * JumpForce, ForceMode.Force);
            JumpForce -= JumpForceReductionRate;
            JumpCount--;
            bCheckCharacterOntheGround = false;
        }

        else if (bCheckCharacterOntheGround)
        {
            float StiffTime = JumpHeight * 0.1f;
            JumpCount = BaseJumpCount;
            JumpForce = BaseJumpForce;
            StartCoroutine(Stiff(StiffTime));
        }
    }


    public Vector3 GetCharacterAxis()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        return new Vector3(h, 0, v).normalized;    
    }


    void CharacterRotate()
    {
        Quaternion r = Quaternion.Euler(CameraMovement.GetCharacterCameraRotationVector());
        float Ry = Quaternion.Lerp(transform.rotation, r, 0.03f).eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, Ry, 0);
    }

    public Vector3 GetCharacterPostion()
    {
        return transform.position;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            bCheckCharacterOntheGround = true;
        }
    }

    IEnumerator Stiff(float Sec)
    {
        Velocity *= 0.0f;
        yield return new WaitForSeconds(Sec);
        Velocity = BaseVelocity;
    }

    public RaycastHit CharacterRayCastInfo(Vector3 Dir, GameObject Object, float Dist)
    {
        RaycastHit hit;
        Physics.Raycast(Object.transform.position, Dir, out hit, Dist);

        return hit;
    }
}
