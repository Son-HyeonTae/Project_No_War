using System.Collections;
using UnityEngine;

public class DocumentCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject       player;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private GameObject       blueAnimation;
    [SerializeField]
    private GameObject       redAnimation;

    private TimeLimit        timeLimit;

    private void Awake() {
        timeLimit = player.GetComponent<TimeLimit>();
    }

    private IEnumerator OnTriggerExit2D(Collider2D collision) {
        // Correct
        if      (collision.gameObject.CompareTag("DocumentBlue") && playerController.isRed == false) {
            timeLimit.IncreaseTime();
            Instantiate(blueAnimation,  new Vector3(0.4f, 0.3f, 0), Quaternion.identity);
        }
        else if (collision.gameObject.CompareTag("DocumentRed" ) && playerController.isRed == true ) {
            timeLimit.IncreaseTime();
            Instantiate(redAnimation,   new Vector3(-0.5488f, 0.2f, 0), Quaternion.identity);
        }
        // Incorrect
        else {
            timeLimit.ReduceTime();
        }
        
        Destroy(collision.gameObject);
        yield return new WaitForSeconds(0.1f);
    }
}
