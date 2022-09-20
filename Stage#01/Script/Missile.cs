using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private float speed;
    [SerializeField]
    private bool           isFront = false;
    [SerializeField]
    private bool           isFirst = false;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (isFront == true) {
            StartCoroutine("TwinkleLoop");
        }
    }

    public void Update () {
        float speed = 2.3f * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(0f, 7.0f, 0f), speed);
    }

    private IEnumerator TwinkleLoop() {
        while (true) {
            if (isFirst) {
                yield return new WaitForSeconds(0.1f);
            }

            Color color = spriteRenderer.color;
            if (color.a == 255f) {
                color.a = 0f;
            }
            else {
                color.a = 255;
            }
            spriteRenderer.color = color;
        yield return null;
        }
    }
}
