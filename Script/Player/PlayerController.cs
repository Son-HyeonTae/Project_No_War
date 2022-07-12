using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public int count = 50;
    public bool lastKey = false;

    public GameObject keyLeft;
    public GameObject keyRight;
    public GameObject docSpawner;
    public GameObject textRemains;

    void Update()
    {
        if (count <= 0) {
            textRemains.GetComponent<RemainText>().GameClear();
        }

        if (Input.GetMouseButtonDown(0)) {
            lastKey = false;
            count--;

            StartCoroutine("PressLeft");
        }
        else if (Input.GetMouseButtonDown(1)) {
            lastKey = true;
            count--;

            StartCoroutine("PressRight");
        }
    }

    private IEnumerator PressLeft() {
        docSpawner.GetComponent<DocumentSpawner>().SpawnDocuments();
        
        keyLeft.GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.1f);
        keyLeft.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    private IEnumerator PressRight() {
        docSpawner.GetComponent<DocumentSpawner>().SpawnDocuments();

        keyRight.GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.1f);
        keyRight.GetComponent<SpriteRenderer>().color = Color.red;
    }
}