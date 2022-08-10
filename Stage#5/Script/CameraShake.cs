using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 0.125f;

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
        Move();
    }

    public void Move()
    {

        if(target.transform.position.x >= -1.63 && target.transform.position.x <= 1.63)
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
