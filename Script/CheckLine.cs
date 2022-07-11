using System.Collections;
using UnityEngine;

public class CheckLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(collision.gameObject);
    }
}
