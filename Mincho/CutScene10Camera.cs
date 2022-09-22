using System.Collections;
using UnityEngine;

public class CutScene10Camera : MonoBehaviour {
    [SerializeField]
    private GameObject[] pinPos;
    [SerializeField]
    private float moveSpeed = 1.0f;
    private int   pinNum    = 0;

    void Start() {
        transform.position = pinPos[pinNum].transform.position;
    }

    void Update() {
        MovePath();
    }

    public void MovePath() {
        transform.position = Vector3.MoveTowards(transform.position, pinPos[pinNum].transform.position, moveSpeed * Time.deltaTime);

        if (transform.position == pinPos[pinNum].transform.position) {
            pinNum++;
        }

        if (pinNum == pinPos.Length) {
            pinNum = 0;
        }
    }
}
