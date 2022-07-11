using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public int Count = 50;

    public GameObject keyLeft;
    public GameObject keyRight;
    public GameObject docSpawner;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            StartCoroutine("PressLeft");
            
            Count--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            StartCoroutine("PressRight");
            
            Count--;
        }
    }

    private IEnumerator PressLeft() {
        docSpawner.GetComponent<DocumentSpawner>().SpawnDocuments();
        docSpawner.GetComponent<DocumentSpawner>().MoveDocs();
        
        keyLeft.GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.1f);
        keyLeft.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    private IEnumerator PressRight() {
        docSpawner.GetComponent<DocumentSpawner>().SpawnDocuments();
        docSpawner.GetComponent<DocumentSpawner>().MoveDocs();

        keyRight.GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.1f);
        keyRight.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
