using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSegment 
{
    
    static readonly GameObject LineParent = new GameObject{ name = "LineSegmentCatalog"};
    readonly GameObject LineSegmentGameObject = new GameObject();
    
    public LineSegment(PointC p1, PointC p2)
    {
        LineSegmentGameObject.transform.parent = LineParent.transform;
        LineSegmentGameObject.name = "LineSegment";
        int intermediatePoints = (int)MathS.ArcLength(p1, p2);
        var points = MathS.GetIntermeditatePoints(p1,p2, intermediatePoints);
        var lr = LineSegmentGameObject.AddComponent<LineRenderer>();
        lr.startWidth = 0.015f;
        lr.endWidth = 0.015f;
        lr.material.color = Color.red;
        lr.positionCount = points.Length;
        lr.SetPositions(points);
    }

    public void Destroy()
    {
        GameObject.Destroy(LineSegmentGameObject);
    }
    

}
