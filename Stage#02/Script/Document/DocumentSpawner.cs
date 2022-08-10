using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] documentArray;

    private void Awake() {
        for (int i = 0; i < 5 ; i++) {
            SpawnDocuments();
        }
    }

    public void SpawnDocuments() {
        int index = Random.Range(0, documentArray.Length);
        Vector3 position = new Vector3(+2.5f, 2.0f, 0);

        Instantiate(documentArray[index], position, Quaternion.identity);

        GameObject[] reds = GameObject.FindGameObjectsWithTag("DocumentRed");
        GameObject[] blues = GameObject.FindGameObjectsWithTag("DocumentBlue");

        for (int i = 0; i < reds.Length; ++i) {
            reds[i].GetComponent<DocumentMovement>().documentMove();
        }

        for (int i = 0; i < blues.Length; ++i) {
            blues[i].GetComponent<DocumentMovement>().documentMove();
        }
    }
}
