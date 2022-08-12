using UnityEngine;
using UnityEngine.Rendering;

public class PieceControl : MonoBehaviour {
    [SerializeField]
    private GameObject BackPiece;

    private Vector3    RightPosition;
    public  bool       InRightPosition;
    public  bool       Selected;

    void Start() {
        RightPosition = transform.position;    
        transform.position = new Vector3(Random.Range(-1.78f, 1.78f), Random.Range(-2.55f, -4.17f));
    }

    void Update() { 
        if (Vector3.Distance(transform.position, RightPosition) < 0.5f) {
            if (!Selected) {
                if (InRightPosition == false) {
                    transform.position = RightPosition;
                    InRightPosition = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                }
            }
        }
    }
}
