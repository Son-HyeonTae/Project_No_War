using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentMovement : MonoBehaviour
{
    public void documentMove() {
        transform.position += new Vector3(-0.5f, -0.5f, 0);
        Color c = this.GetComponent<SpriteRenderer>().color;
        c.a += 0.1999f;
        this.GetComponent<SpriteRenderer>().color = c;
    }

    public void clearBlue() {
        transform.position += new Vector3(-1f, -2f, 0);
    }

    public void clearRed() {
        transform.position += new Vector3(+2f, -2f, 0);
    }
}
