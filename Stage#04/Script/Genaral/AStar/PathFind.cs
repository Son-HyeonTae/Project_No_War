using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// A* Algorithms
/// 
/// GCost -> path cost from Current Node to Neighbour Node
/// HCost -> Esimated path Cost from Neighbour Node to target Node
/// FCost -> G + H
/// 
/// OpenList -> A set of searchable nodes
/// CloseList -> A set of already been scanned
/// 
/// Functions--
/// private List<Node> GetNeighbourList(Node currentNode) {}; 
/// </summary>


///                       ┌----<-5--<-----┐
///StartFindPath() 1-> FindPath() 2->> RetracePath() 3->> SimplifyPath() ->--┐
///                         |                     └-----<------<-----4---<---┘
///                         V
///                         PathRequestManager.Instance.FinishedProcessingPath(waypoints, pathSuccess);

public class PathFind : MonoBehaviour
{
    Grid grid;
    private Coroutine CurrentRunningCoroutine;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    public void StartFindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        CurrentRunningCoroutine = StartCoroutine(FindPath(startPosition, targetPosition));
    }

    public void StopFindPath()
    {
        StopCoroutine(CurrentRunningCoroutine);
    }

    IEnumerator FindPath(Vector3 StartPostion, Vector3 TargetPosition)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;
        int c = 0;

        Node StartNode = grid.NodeFromWorldPoint(StartPostion);
        Node TargetNode = grid.NodeFromWorldPoint(TargetPosition);

        if(StartNode.bWalkAble && TargetNode.bWalkAble)
        {
            Heap<Node> OpenSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> CloseSet = new HashSet<Node>();

            OpenSet.Add(StartNode);

            while (OpenSet.Count > 0)
            {
                c++;
                Node CurrentNode = OpenSet.RemoveFirst();
                CloseSet.Add(CurrentNode);

                if (CurrentNode == TargetNode)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (Node neighbour in grid.GetNeighbours(CurrentNode))
                {
                    if (!neighbour.bWalkAble || CloseSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int NewMovementCostToNeighbour = CurrentNode.GCost + GetDistance(CurrentNode, neighbour);
                    if (NewMovementCostToNeighbour < neighbour.GCost || !OpenSet.Contains(neighbour))
                    {
                        neighbour.GCost = NewMovementCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, TargetNode);
                        neighbour.Parent = CurrentNode;

                        if (!OpenSet.Contains(neighbour))
                        {
                            OpenSet.Add(neighbour);
                        }
                        else
                        {
                            OpenSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess)
        {
            waypoints = RetracePath(StartNode, TargetNode);
        }
        //Process player movements with the obtained travel path information and the success of FindPath
        PathRequestManager.Instance.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector3[] RetracePath(Node StartNode, Node EndNode)
    {
        List<Node> Path = new List<Node>();
        Node CurrentNode = EndNode;

        while(CurrentNode != StartNode)
        {
            Path.Add(CurrentNode);
            CurrentNode = CurrentNode.Parent;
        }
        Vector3[] waypoints = SimplifyPath(Path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();

        for (int i = 1; i < path.Count; i++)
        {
            waypoints.Add(path[i].WorldPosition);
        }
        return waypoints.ToArray();
    }

    int GetDistance(Node A, Node B)
    {
        int dstX = Mathf.Abs(A.GridX - B.GridX);
        int dstY = Mathf.Abs(A.GridY - B.GridY);

        if(dstX > dstY)
            return 14*dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}