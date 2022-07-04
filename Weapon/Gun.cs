using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Projectile     projectile;
    [SerializeField] private Transform      head;
    [SerializeField] private float          FireDelay;
    private bool bEndDelay;

    private void Awake()
    {
        bEndDelay = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && bEndDelay)
        {
            StartCoroutine(InstanceProjectile());
            Instantiate(projectile, head.position, head.rotation);
        }
    }

    IEnumerator InstanceProjectile()
    {
        bEndDelay = false;
        yield return new WaitForSeconds(FireDelay);
        bEndDelay = true;
    }
}
