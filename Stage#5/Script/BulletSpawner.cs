using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] shotPosition;
    [SerializeField] private GameObject[] shotEndPosition;
    private LineRenderer bulletLineRenderer;
    private AudioSource bulletFireListen;

    // lastTime
    // private float lastFireTime;

    void Start()
    {
        // lastFireTime = Time.time;

        bulletFireListen = GetComponent<AudioSource>();
        bulletFireListen.enabled = false;

        bulletLineRenderer = GetComponent<LineRenderer>();
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
        
    }


    // void Update()
    // {
    //     // 임시로 random 설정.
    //     if(Time.time - lastFireTime > 7f)
    //     {   
    //         StartCoroutine(Fire(Random.Range(0, 4)));
    //         lastFireTime = Time.time;
    //     }
        
    // }

    public void SpawnBullet(int line)
    {
        StartCoroutine(Fire(line));
    }
    
    private IEnumerator Fire(int line)
    {
        GMScene5.instance.EnableWarning(line - 1);
        // 3s 대기
        yield return new WaitForSeconds(4f);
        GMScene5.instance.DisableWarning(line - 1);

        // 위치 저장.
        Vector2 shotPos = shotPosition[line - 1].transform.position;
        Vector2 endPos = shotEndPosition[line - 1].transform.position;
        //발사 방향
        Vector2 shotDirection = (endPos - shotPos).normalized;
        //hit 위치
        Vector2 hitPos = Vector2.zero;

        for(int i = 0; i < 3; i++)
        {
            
            // Debug.Log("원래: " + shotPos + "가짜: " );
            RaycastHit2D[] raycastHit = Physics2D.RaycastAll(shotPos, shotDirection);
            
            if(raycastHit[0].collider != null) // 충둘할 경우
            {
                for(int j = 0; j < raycastHit.Length; j++)
                {
                    Player target = raycastHit[j].collider.GetComponent<Player>();
                    // Debug.Log(raycastHit[j].collider.name);
                    if (target != null && target.carState == Player.State.Normal) // 충돌한 물체가 player일 경우 life -1
                    {
                        Debug.Log("player 목숨: " + target.Damage());
                        // hitPos = raycastHit.point;
                    }
                }

            }
            else // 충돌 안함.
            {
                Debug.Log("충돌 안함.");
            }

            // <-- 궤적 그리기 -->
            // 발사 위치 지정.
            bulletLineRenderer.SetPosition(0, shotPos);
            bulletLineRenderer.SetPosition(1, endPos);
            // 궤적 표시.
            bulletLineRenderer.enabled = true;
            bulletFireListen.enabled = true;
            yield return new WaitForSeconds(0.15f);
            bulletLineRenderer.enabled = false;
            bulletFireListen.enabled = false;
            yield return new WaitForSeconds(0.3f);

        }

    }

}
