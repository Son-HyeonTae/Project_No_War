using System.Collections;
using UnityEngine;

public class DocumentCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject       player;
    [SerializeField]
    private PlayerController playerController;
    
    private IEnumerator OnTriggerExit2D(Collider2D collision) {
        // Correct
        if (((collision.gameObject.CompareTag("DocumentBlue")) && (playerController.isRed == false))
         || ((collision.gameObject.CompareTag("DocumentRed" )) && (playerController.isRed == true ))) {
            player.GetComponent<TimeLimit>().IncreaseTime();
            Debug.Log("Correct");
        }
        // Incorrect
        else {
            player.GetComponent<TimeLimit>().ReduceTime();
            Debug.Log("Incorrect");
        }
        
        Destroy(collision.gameObject);
        yield return new WaitForSeconds(0.1f);
    }
}
