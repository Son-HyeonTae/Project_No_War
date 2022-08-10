using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    const float VerticalLineGradient = 1e5f;

    private float Gradient;
    private float Y_Intercept;
    private Vector2 PointOnLine_1;
    private Vector2 PointOnLine_2;

    private bool ApproachSide;

    private float GradientPerpendicular;

    public Line(Vector2 PointOnLine, Vector2 PointPerpendicularToLine)
    {
        float dx = PointOnLine.x - PointPerpendicularToLine.x;
        float dy = PointOnLine.y - PointPerpendicularToLine.y;

        if(dx == 0)
            GradientPerpendicular = VerticalLineGradient;
        else
            GradientPerpendicular = dy / dx;


        if (GradientPerpendicular == 0)
            Gradient = VerticalLineGradient;
        else
            Gradient = -1 / GradientPerpendicular;

        Y_Intercept = PointOnLine.y - Gradient * PointOnLine.x;
        PointOnLine_1 = PointOnLine;
        PointOnLine_2 = PointOnLine + new Vector2(1, Gradient);

        ApproachSide = GetSide(PointPerpendicularToLine);

    }

    bool GetSide(Vector2 p)
    {
        return (p.x - PointOnLine_1.x) * (PointOnLine_2.y - PointOnLine_1.y) > (p.y - PointOnLine_1.y) * (PointOnLine_2.x - PointOnLine_1.x);
    }

    public bool HasCrossedLine(Vector2 p)
    {
        return GetSide(p) != ApproachSide;
    }
}
