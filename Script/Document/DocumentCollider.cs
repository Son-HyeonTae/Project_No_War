using System.Collections;
using UnityEngine;

public class DocumentCollider : MonoBehaviour
{
    [SerializeField]
    private float timeDamage = 5.0f;
    [SerializeField]
    private float timeIncrease = 3.0f;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private PlayerController playerController;
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if ((collision.gameObject.CompareTag("DocumentBlue"))
            && (playerController.lastKey == false)) {
                player.GetComponent<TimeLimit>().IncreaseTime(timeIncrease);
                Debug.Log("True - Blue");
            }
        else if ((collision.gameObject.CompareTag("DocumentRed"))
            && (playerController.lastKey == true)) {
                player.GetComponent<TimeLimit>().IncreaseTime(timeIncrease);
                Debug.Log("True - Red");
        }
        else {
            player.GetComponent<TimeLimit>().ReduceTime(timeDamage);
            Debug.Log("False\nTime -5");
        }
        
        Destroy(collision.gameObject);
    }
}
