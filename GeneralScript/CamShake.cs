using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : Singleton<CamShake>
{
    private Transform ShakeCamera;

    [SerializeField] float SaheTime = 1.0f;
    [SerializeField] float Speed = 2.0f;
    [SerializeField] float Amount = 1.0f;

    private void Awake()
    {
        ShakeCamera = Camera.main.GetComponent<Transform>();
    }

    public void Shake(float time, float speed, float amount)
    {
        StartCoroutine(CorShake(time, speed, amount));
    }

    private IEnumerator CorShake(float time, float speed, float amount)
    {
        Vector3 originPos = ShakeCamera.localPosition;
        float elapsedTime = 0.0f;
        while(elapsedTime < SaheTime)
        {
            Vector3 randomPoint = originPos + Random.insideUnitSphere * Amount;
            ShakeCamera.localPosition = Vector3.Lerp(ShakeCamera.localPosition, randomPoint, Speed * Time.deltaTime);
            yield return null;

            elapsedTime += Time.deltaTime;
        }
        ShakeCamera.localPosition = originPos;
    }
}
