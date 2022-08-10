using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public readonly Vector3[] LookPoints;
    public readonly Line[] TurnBoundaries;
    public readonly int FinishLineIndex;

    public Path(Vector3[] Waypoints, Vector3 StartPos, float TurnDst)
    {
        LookPoints = Waypoints;
        TurnBoundaries = new Line[LookPoints.Length];
        FinishLineIndex = TurnBoundaries.Length - 1;

        Vector2 PreviousPoint = V3ToV2(StartPos);
        for (int i = 0; i < LookPoints.Length; i++)
        {
            Vector2 CurrentPoint = V3ToV2(LookPoints[i]);
            Vector2 DirToCurrentPoint = (CurrentPoint - PreviousPoint).normalized;
            Vector2 TurnBoundaryPoint = (i==FinishLineIndex) ? CurrentPoint : CurrentPoint - DirToCurrentPoint * TurnDst;

            TurnBoundaries[i] = new Line(TurnBoundaryPoint, PreviousPoint - DirToCurrentPoint * TurnDst);
            PreviousPoint = TurnBoundaryPoint;
        }
    }

    private Vector2 V3ToV2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }
}
