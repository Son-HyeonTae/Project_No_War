using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ------------------------------------Work Flow-----------------------------------------------------
/// | 1. Request path find from object inheriting Mob class --- RequestPath()                        |
/// | 2. Queue the received request and call the TryProcessNext()                                    |
/// | 3. Set the status to 'Processing',                                                             | 
/// |    performing the first request of the queuing queue  --- Call the PathFindClass.StartFindPath |
/// | 4. If the processing is successful, remove from the Move processing and                        |
/// |    Wait Queue of the called class based on the return value of StartFindPath                   |
/// --------------------------------------------------------------------------------------------------
/// </summary>


public class PathRequestManager : Singleton<PathRequestManager>
{
    //Structure that manages requests
    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 Start, Vector3 End, Action<Vector3[], bool> callback)
        {
            pathStart = Start;
            pathEnd = End;
            this.callback = callback;
        }
    }


    private Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();    //Queue to manage PathRequest

    private PathRequest currentPathRequest;

    private PathFind pathFinding; //A* Class
    private bool bProcessingPath; //Is end Path finding process?

    private void Awake()
    {
        currentPathRequest = default;
        bProcessingPath = false;
        pathFinding = GetComponent<PathFind>();
    }

    //Start path find request
    public void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        pathRequestQueue.Enqueue(newRequest);
        TryProcessNext();
    }

    public void TryProcessNext()
    {
        if (bProcessingPath == false && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            bProcessingPath = true;
            pathFinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        bProcessingPath = false;
        TryProcessNext();
    }

    public void StopPathFindingRequest()
    {
        pathFinding.StopFindPath();
    }
}
