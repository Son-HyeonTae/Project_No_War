using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public Node(bool bWlakAble, Vector3 WorldPosition, int GridX, int GridY)
    {
        this.bWalkAble = bWlakAble;
        this.WorldPosition = WorldPosition;
        this.GridX = GridX;
        this.GridY = GridY;
    }
    int heapIndex;

    public bool bWalkAble;
    public Vector3 WorldPosition;

    public int GridX;
    public int GridY;

    public int GCost;
    public int HCost;
    public int FCost
    {
        get
        {
            return GCost + HCost;
        }
    }

    public Node Parent;
    public int CompareTo(Node node)
    {
        int compare = FCost.CompareTo(node.FCost);
        if(compare == 0)
        {
            compare = HCost.CompareTo(node.HCost);
        }
        return -compare;
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }
}
