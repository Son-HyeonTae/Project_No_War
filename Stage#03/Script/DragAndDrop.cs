using UnityEngine;
using UnityEngine.Rendering;

public class DragAndDrop : MonoBehaviour {
    private GameObject[] Puzzles;
    public GameObject SelectedPiece;
    int OrderInLayer = 1;

    void Start() {
        Puzzles = GameObject.FindGameObjectsWithTag("Puzzle");
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);

            if (hit.transform.CompareTag("Puzzle")) {
                if (!hit.transform.GetComponent<PieceControl>().InRightPosition) {
                    SelectedPiece = hit.transform.gameObject;
                    SelectedPiece.GetComponent<PieceControl>().Selected = true;
                    SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OrderInLayer;
                    OrderInLayer++;
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            if (SelectedPiece != null) {
                SelectedPiece.GetComponent<PieceControl>().Selected = false;
                SelectedPiece = null;
            }
        }

        if (SelectedPiece != null) {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(MousePoint.x, MousePoint.y, 0);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0) {
            for (int i=0; i < Puzzles.Length; i++) {
                Color color = Puzzles[i].GetComponent<SpriteRenderer>().color;
                if (color.a == 0) {
                    color.a = 255f;
                    Puzzles[i].GetComponent<SpriteRenderer>().color = color;
                }
                else {
                    color.a = 0f;
                    Puzzles[i].GetComponent<SpriteRenderer>().color = color;
                }
            }
        }
    }
}
