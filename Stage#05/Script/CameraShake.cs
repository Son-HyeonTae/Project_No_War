using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // [SerializeField] private float moveSpeed = 0.125f; 
    /**
    * @최종 수정자 - 민초
    * @최종 수정일 - 2022-09-23::11:43
    * moveSpeed 변수 Console 창에 자꾸 not Assigned 경고 떠서 주석처리 해둡니다... ㅠㅠ
    * 확인하시고 변수 필요없으시면 이 주석이랑 같이 지워주시면 감사하겠습니다 :)
    */

    [SerializeField] private GameObject player;

    private Transform target;
    private Player playerScript;

    private Vector2 desiredPos;
    private Vector2 smoothedPos;

    private void Awake() 
    {
        target = player.GetComponent<Transform>();
        playerScript = player.GetComponent<Player>();
    }

    private void Update() 
    {
        // Move();
    }

    public void Move()
    {

        if(target.transform.position.x >= -1.06f && target.transform.position.x <= 1.03f)
        {
            transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z );
        }

    }

    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;

    }
}
