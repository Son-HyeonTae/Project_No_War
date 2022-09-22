using System.Collections;
using UnityEngine;

public class CutScene10 : MonoBehaviour {
    [SerializeField]
    private GameObject[] images;
    
    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("SceneProcess");
    }

    private IEnumerator SceneProcess() {
        for(int i = 0; i < 3; i++) {
            Color playerColor = images[i].GetComponent<SpriteRenderer>().color;
            for (int a = 0; a < 2; a++) {
                for(float c = 0f; c <= 40f; c += 0.4f) {
                    playerColor.r = (150f+c)/255f;
                    playerColor.g = (150f-c)/255f;
                    playerColor.b = (150f-c)/255f;
                    playerColor.a = (160f-c)/160f;
                    images[i].GetComponent<SpriteRenderer>().color = playerColor;
                    yield return new WaitForSeconds(0.01f);
                }
                for(float c = 0f; c <= 40f; c += 0.4f) {
                    playerColor.r = (190f-c)/255f;
                    playerColor.g = (110f+c)/255f;
                    playerColor.b = (110f+c)/255f;
                    playerColor.a = (120f+c)/160f;
                    images[i].GetComponent<SpriteRenderer>().color = playerColor;
                    yield return new WaitForSeconds(0.01f);
                }
            }

            for(float j = 1.0f; j >= 0.0f; j -= 0.01f) {
                playerColor.a = j;
                images[i].GetComponent<SpriteRenderer>().color = playerColor;
                yield return new WaitForSeconds(0.01f);
            }

            images[i].SetActive(false);
        }

        yield return new WaitForSeconds(1f);

        GameManager.Instance.LoadStage(()=>{return true;}, GameManager.Instance.Stage5, 0f);
    }
}
