using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentMovement : MonoBehaviour
{
    public void documentMove() {
    transform.position += new Vector3(-0.5f, -0.5f, 0);
    }
}