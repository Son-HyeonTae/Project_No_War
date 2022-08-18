using UnityEngine;
using UnityEngine.Rendering;

public class PieceControl : MonoBehaviour {
    [SerializeField]
    public GameObject BackPiece;

    public Vector3    RightPosition;
    public  bool      Selected;
    public  bool      InRightPosition;
    public  bool      IsGameStart = false;

    void Awake() {
        RightPosition = transform.position;   
        transform.position = new Vector3(Random.Range(-1.78f, 1.78f), Random.Range(-2.55f, -4.17f));
    }

    void Update() {
        // Find Right Position
        if (Vector3.Distance(transform.position, RightPosition) < 0.5f) {
            if (!Selected) {
                if (InRightPosition == false) {
                    transform.position = RightPosition;
                    InRightPosition = true;
                    BackPiece.GetComponent<BackPieceControl>().InRightPosition = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                }
            }
        }

        if (InRightPosition == true) {
            transform.position = RightPosition;
        }

        IsGameStart = true;
    }

    void OnEnable() {
        if (!InRightPosition && IsGameStart) {
            transform.position = new Vector3(BackPiece.GetComponent<Transform>().position.x, BackPiece.GetComponent<Transform>().position.y, 0);
        }
    }
}
