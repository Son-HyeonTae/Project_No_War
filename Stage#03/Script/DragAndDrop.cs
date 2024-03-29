using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Collections;

public class DragAndDrop : MonoBehaviour {
    [SerializeField]
    private GameObject   ClearButton;
    [SerializeField]
    private GameObject   TablePuzzle;
    private GameObject[] Puzzles;
    public  GameObject   SelectedPiece;

    private bool ScrollFlag;
    private int  OrderInLayer = 1;
    public  int  CorrectCount = 0;

    void Start() {
        Puzzles = GameObject.FindGameObjectsWithTag("Puzzle");
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
            if (hit.transform.CompareTag("GameController")) {
                GameManager.Instance.LoadStage(() => { return true; }, GameManager.Instance.CutScene08);
            }

            if (hit.transform.CompareTag("Puzzle")) {
                if (!hit.transform.GetComponent<PieceControl>().InRightPosition) {
                    SelectedPiece = hit.transform.gameObject;
                    SelectedPiece.GetComponent<PieceControl>().Selected = true;
                    SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OrderInLayer;
                    OrderInLayer++;
                }
            }

            if (hit.transform.CompareTag("BackPuzzle")) {
                if (!hit.transform.GetComponent<BackPieceControl>().InRightPosition) {
                    SelectedPiece = hit.transform.gameObject;
                    SelectedPiece.GetComponent<BackPieceControl>().Selected = true;
                    SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OrderInLayer;
                    OrderInLayer++;
                }
            }

        }

        if (Input.GetMouseButtonUp(0)) {
            if (SelectedPiece != null && SelectedPiece.transform.CompareTag("Puzzle")) {
                SelectedPiece.GetComponent<PieceControl>().Selected = false;
                SelectedPiece = null;
            }

            if (SelectedPiece != null && SelectedPiece.transform.CompareTag("BackPuzzle")) {
                SelectedPiece.GetComponent<BackPieceControl>().Selected = false;
                SelectedPiece = null;
            }
        }

        if (SelectedPiece != null) {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(MousePoint.x, MousePoint.y, 0);
        }

        if ((Input.GetAxis("Mouse ScrollWheel") != 0) && (ScrollFlag == false)) {
            ScrollFlag = true;
            StartCoroutine(FlipOver());
        }

        if (CorrectCount >= 70) {
            
            ClearButton.SetActive(true);
        }



        ///HotKey
        ///
        if(Input.GetKeyDown(KeyCode.F1))
        {
            GameManager.Instance.LoadStage(() => { return true; }, GameManager.Instance.CutScene08);
        }
    }

    IEnumerator FlipOver() {
        for (int i=0; i<18; i++) {
            TablePuzzle.GetComponent<Transform>().Rotate(0, 10, 0);
            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < Puzzles.Length; i++) {
                if (Puzzles[i].activeSelf == true) {
                    Puzzles[i].SetActive(false);
                    Puzzles[i].GetComponent<PieceControl>().BackPiece.SetActive(true);
                }
                else {
                    Puzzles[i].SetActive(true);
                    Puzzles[i].GetComponent<PieceControl>().BackPiece.SetActive(false);
                }
        }

        ScrollFlag = false;
    }
}
