using UnityEngine;
using UnityEngine.Rendering;

public class BackPieceControl : MonoBehaviour {
    [SerializeField]
    private GameObject FrontPiece;

    public Vector3     RightPosition;
    public  bool       Selected;
    public  bool       InRightPosition;

    void Awake() {
        RightPosition = transform.position;    
        transform.position = new Vector3(FrontPiece.GetComponent<Transform>().position.x, FrontPiece.GetComponent<Transform>().position.y, 1);
    }

    void Update() {
        if (Vector3.Distance(transform.position, RightPosition) < 0.5f) {
            if (!Selected) {
                if (InRightPosition == false) {
                    transform.position = RightPosition;
                    InRightPosition = true;
                    FrontPiece.GetComponent<PieceControl>().InRightPosition = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                }
            }
        }

        if (InRightPosition == true) {
            transform.position = RightPosition;
        }
    }

    void OnEnable() {
        if (!InRightPosition) {
            transform.position = new Vector3(FrontPiece.GetComponent<Transform>().position.x, FrontPiece.GetComponent<Transform>().position.y, 1);
        }
    }
}
