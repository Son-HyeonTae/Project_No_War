using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 2D Grid Class
/// n X m size create Grid
/// 
/// </summary>


public class Grid : MonoBehaviour
{
    public LayerMask lmUnWalkAble;
    public Vector3 GridWorldSize;
    public float NodeRadius;
    Node[,] GridNode;

    float NodeDiameter;
    int GridSizeX;
    int GridSizeY;
    Vector3 WorldBottomLeft;

    public bool DisplayGridGizmos;
    public int MaxSize { get { return GridSizeX * GridSizeY; } }


    private void Awake()
    {
        NodeDiameter = NodeRadius * 2;
        GridSizeX = Mathf.RoundToInt(GridWorldSize.x /NodeDiameter);
        GridSizeY = Mathf.RoundToInt(GridWorldSize.y /NodeDiameter);
        CreateGrid();
    }

    /*private void Update()
    {
        WorldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.up * GridWorldSize.y / 2;

        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                Vector3 WorldPoint = WorldBottomLeft + Vector3.right * (x * NodeDiameter + NodeRadius) + Vector3.up * (y * NodeDiameter + NodeRadius);
                bool Walkable = !(Physics2D.OverlapBox(WorldPoint, new Vector3(NodeDiameter, NodeDiameter), 0, lmUnWalkAble));

                if(GridNode[x,y] != null)
                    GridNode[x, y].bWalkAble = Walkable;
            }
        }
    }*/

    void CreateGrid()
    {
        GridNode = new Node[GridSizeX, GridSizeY];
        WorldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.up * GridWorldSize.y / 2;

        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                Vector3 WorldPoint = WorldBottomLeft + Vector3.right * (x * NodeDiameter + NodeRadius) + Vector3.up * (y * NodeDiameter + NodeRadius);
                bool Walkable = !(Physics2D.OverlapBox(WorldPoint, new Vector3(NodeDiameter, NodeDiameter), 0, lmUnWalkAble));

                GridNode[x, y] = new Node(Walkable, WorldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.GridX + x;
                int checkY = node.GridY + y;

                if(checkX >= 0 && checkX < GridSizeX && checkY >= 0 && checkY < GridSizeY)
                {
                    neighbours.Add(GridNode[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    //Convert Grid Position to WorldPosition
    public Node NodeFromWorldPoint(Vector3 WorldPosition)
    {
        float PercentX = (WorldPosition.x + GridWorldSize.x / 2) / GridWorldSize.x;
        float PercentY = (WorldPosition.y + GridWorldSize.y / 2) / GridWorldSize.y;
        PercentX = Mathf.Clamp01(PercentX); //0~1
        PercentY = Mathf.Clamp01(PercentY);// 0~1

        int x = Mathf.RoundToInt((GridSizeX - 1) * PercentX);
        int y = Mathf.RoundToInt((GridSizeY - 1) * PercentY);
        return GridNode[x, y];
    }



    private void OnDrawGizmos()
    {
        if (GridNode != null && DisplayGridGizmos)
        {
            foreach (Node Node in GridNode)
            {
                Gizmos.color = (Node.bWalkAble) ? Color.white : Color.red;
                Gizmos.DrawCube(Node.WorldPosition, Vector3.one * (NodeDiameter - .1f));
            }
        }
    }
}
