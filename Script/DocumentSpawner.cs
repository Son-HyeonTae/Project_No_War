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
            MoveDocs();
        }
    }

    public void SpawnDocuments() {
        int index = Random.Range(0, documentArray.Length);
        Vector3 position = new Vector3(2.5f, 2.0f, 0);

        Instantiate(documentArray[index], position, Quaternion.identity);
    }

    public void MoveDocs() {
        GameObject[] docs = GameObject.FindGameObjectsWithTag("Document");

        for (int i = 0; i < docs.Length; ++i) {
            docs[i].GetComponent<PrefabMovement>().prefabMove();
        }
    }
}
