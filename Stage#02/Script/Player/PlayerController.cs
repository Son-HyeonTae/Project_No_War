using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject stampAnimation;
    public  GameObject docSpawner;
    public  GameObject textRemains;
    private Animator   animator;

    [SerializeField]
    public int  count = 80;
    public bool isRed = false;

    void Update()
    {
        if (count <= 0) {
            textRemains.GetComponent<RemainText>().GameClear();
        }

        if (Input.GetMouseButtonDown(0)) {
            Instantiate(stampAnimation, new Vector3(0, 0, 0), Quaternion.identity);
            isRed = false;
            docSpawner.GetComponent<DocumentSpawner>().SpawnDocuments();
            if (count >= 1) {
                count--;
            }
        }
        else if (Input.GetMouseButtonDown(1)) {
            Instantiate(stampAnimation, new Vector3(0, 0, 0), Quaternion.identity);
            isRed = true;
            docSpawner.GetComponent<DocumentSpawner>().SpawnDocuments();
            if (count >= 1) {
                count--;
            }
        }
    }
}