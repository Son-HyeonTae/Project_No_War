using UnityEngine;

public class CutScene04 : MonoBehaviour {
    [SerializeField]
    private Animator player;

    public void end() {
        GameManager.Instance.LoadStage(()=>{return true;}, GameManager.Instance.CutScene5, 0f);
    }
}
