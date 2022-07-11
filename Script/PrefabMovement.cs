using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabMovement : MonoBehaviour
{
    public void prefabMove() {
    transform.position += new Vector3(-0.5f, -0.5f, 0);
    }
}
