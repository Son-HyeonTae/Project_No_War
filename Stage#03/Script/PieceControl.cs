using UnityEngine;
using UnityEngine.Rendering;

public class PieceControl : MonoBehaviour {
    [SerializeField]
    public  GameObject BackPiece;
    [SerializeField]
    private GameObject Player;

    public Vector3    RightPosition;
    public bool       Selected;
    public bool       CorrectCheck;
    public bool       InRightPosition;
    public bool       IsGameStart;

    void Awake() {
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(-3880.0f, 3880.0f), Random.Range(-5200.0f, -8000.0f));
    }

    void Update() {
        // Find Right Position
        if (Vector3.Distance(transform.position, RightPosition) < 2100.0f) {
            if (!Selected) {
                if (InRightPosition == false) {
                    transform.position = RightPosition;
                    InRightPosition = true;
                    BackPiece.GetComponent<BackPieceControl>().InRightPosition = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                }
            }
        }

        if (InRightPosition == true && CorrectCheck == false) {
            CorrectCheck = true;
            transform.position = RightPosition;
            Destroy(GetComponent<BoxCollider2D>());
            Player.GetComponent<DragAndDrop>().CorrectCount += 1;
        }

        IsGameStart = true;
    }

    void OnEnable() {
        if (!InRightPosition && IsGameStart) {
            transform.position = new Vector3(BackPiece.GetComponent<Transform>().position.x, BackPiece.GetComponent<Transform>().position.y, 0);
        }
    }
}
