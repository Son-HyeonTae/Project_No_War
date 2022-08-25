using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public int  count = 80;
    public bool isRed = false;

    public GameObject docSpawner;
    public GameObject textRemains;

    void Update()
    {
        if (count <= 0) {
            textRemains.GetComponent<RemainText>().GameClear();
        }

        if (Input.GetMouseButtonDown(0)) {
            isRed = false;
            count--;
            docSpawner.GetComponent<DocumentSpawner>().SpawnDocuments();
        }
        else if (Input.GetMouseButtonDown(1)) {
            isRed = true;
            count--;
            docSpawner.GetComponent<DocumentSpawner>().SpawnDocuments();
        }
    }
}