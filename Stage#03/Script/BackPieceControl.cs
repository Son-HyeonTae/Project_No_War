using UnityEngine;
using UnityEngine.Rendering;

public class BackPieceControl : MonoBehaviour {
    [SerializeField]
    private GameObject FrontPiece;
    [SerializeField]
    private GameObject Player;

    public Vector3     RightPosition;
    public bool        Selected;
    public bool        CorrectCheck;
    public bool        InRightPosition;

    void Awake() {
        RightPosition = transform.position;    
        transform.position = new Vector3(FrontPiece.GetComponent<Transform>().position.x, FrontPiece.GetComponent<Transform>().position.y, 1);
    }

    void Update() {
        if (Vector3.Distance(transform.position, RightPosition) < 2100.0f) {
            if (!Selected) {
                if (InRightPosition == false) {
                    transform.position = RightPosition;
                    InRightPosition = true;
                    FrontPiece.GetComponent<PieceControl>().InRightPosition = true;
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
    }

    void OnEnable() {
        if (!InRightPosition) {
            transform.position = new Vector3(FrontPiece.GetComponent<Transform>().position.x, FrontPiece.GetComponent<Transform>().position.y, 1);
        }
    }
}
