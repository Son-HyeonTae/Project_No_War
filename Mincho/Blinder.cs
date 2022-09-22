using System.Collections;
using UnityEngine;

public class Blinder : MonoBehaviour {
    [SerializeField]
    private GameObject[] images;
    
    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("SceneProcess");
    }

    private IEnumerator SceneProcess() {
        yield return new WaitUntil(() => {return GameManager.Instance.bLoadedScene;});
        
        for(int i = 0; i < 2; i++) {
            yield return new WaitForSeconds(0.5f);
            Color color = spriteRenderer.color;

            for(float j = 0.0f; j <= 1.0f; j += 0.01f)
            {
                color.a = j;
                spriteRenderer.color = color;
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.5f);

            images[i].SetActive(false);

            for(float j = 1.0f; j >= 0.0f; j -= 0.01f)
            {
                color.a = j;
                spriteRenderer.color = color;
                yield return new WaitForSeconds(0.01f);
            }
        }

        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.LoadStage(()=>{return true;}, GameManager.Instance.Stage3, 0f);
    }
}
