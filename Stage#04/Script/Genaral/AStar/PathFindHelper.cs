using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindHelper : MonoBehaviour
{
    //--AStar
    private Vector3[] path;
    private int targetIndex;
    private Coroutine LastRoutine;

    public bool bEndPathFinding { get; private set; } = true;
    public float Velocity { get; set; }

    public void StartPathFind(Vector3 Start, Vector3 End)
    {
        if (!bEndPathFinding) 
            return;
        PathRequestManager.Instance.RequestPath(Start, End, OnPathFound);
        bEndPathFinding = false;
    }
    public void StopPathFinding()
    {
        if (LastRoutine != null)
        {
            PathRequestManager.Instance.StopPathFindingRequest();
            StopCoroutine(LastRoutine);
        }
        path = null;
        targetIndex = 0;

        bEndPathFinding = true;
    }

    private void OnPathFound(Vector3[] Waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = Waypoints;
            StopCoroutine(FollowPath());
            LastRoutine = StartCoroutine(FollowPath());
        }
        else
        {
            path = null;
        }
    }


    //Move To Path
    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    bEndPathFinding = true;
                    targetIndex = 0;
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint,Velocity * Time.deltaTime);
            yield return null;
        }
    }
}
