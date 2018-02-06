
using UnityEngine;

public static class MathS
{
    public static Vector3 CartToSpher(Vector3 cartesianCoordinates)
    {
        float x = cartesianCoordinates.x;
        float y = cartesianCoordinates.y;
        float z = cartesianCoordinates.z;
        float r = Mathf.Sqrt(x * x + y * y + z * z);
        float theta = Mathf.Asin(y / r);
        float phi = Mathf.Atan2(z, x);
        return new Vector3(r, theta, phi < 0 ? Mathf.PI * 2 - Mathf.Abs(phi) : phi);
    }
    public static Vector3 SpherToCart(Vector3 sphericalCoordinates)
    {
        float r = sphericalCoordinates.x;
        float theta = sphericalCoordinates.y;
        float phi = sphericalCoordinates.z;
        float x = r * Mathf.Cos(theta) * Mathf.Cos(phi);
        float z = r * Mathf.Cos(theta) * Mathf.Sin(phi);
        float y = r * Mathf.Sin(theta);
        return new Vector3(x, y, z);
    }

    public static float ArcLengthRad(PointC p1, PointC p2)
    {
        return Mathf.Acos(Mathf.Sin(p1.PhiRad) * Mathf.Sin(p2.PhiRad) +  Mathf.Cos(p1.PhiRad) * Mathf.Cos(p2.PhiRad) * Mathf.Cos(p1.ThetaRad - p2.ThetaRad));
    }

    public static float ArcLength(PointC p1, PointC p2)
    {
        return ArcLengthRad(p1, p2) * p1.R;
    }

    public static float AngleByPoint(PointC p1, PointC vertex, PointC p2)
    {
        float a = ArcLengthRad(vertex, p1);
        float b = ArcLengthRad(vertex, p2);
        float c = ArcLengthRad(p1, p2);
        return  Mathf.Acos((Mathf.Cos(b) - Mathf.Cos(c) * Mathf.Cos(a)) / (Mathf.Sin(c) * Mathf.Sin(a)));
    }
    public static float TriangleExcess(PointC p1, PointC p2, PointC p3)
    {
        float A = AngleByPoint(p1, p2, p3);
        float B = AngleByPoint(p3, p1, p2);
        float C = AngleByPoint(p2, p3, p1);
        return A + B + C - Mathf.PI;
    }
    public static float TriangleArea(PointC p1, PointC p2, PointC p3)
    {
        return TriangleExcess(p1, p2, p3) * p1.R;
    }

}
public enum InputType
{
    Degrees,
    Radians,
    Cartesian
}
public class PointC
{
    public readonly float R;
    public readonly float ThetaRad;
    public readonly float PhiRad;
    public float ThetaDeg => ThetaRad * Mathf.Rad2Deg;
    public float PhiDeg => PhiRad * Mathf.Rad2Deg;
    public readonly Vector3 point; 
    public PointC(Vector3 p,InputType type)
    {
        switch (type)
        {
            case InputType.Radians:
                R = p.x;
                ThetaRad = p.y;
                PhiRad = p.z;
                point = MathS.SpherToCart(p);
                break;
            case InputType.Degrees:
                R = p.x;
                ThetaRad = p.y * Mathf.Deg2Rad;
                PhiRad = p.z * Mathf.Deg2Rad;
                point = MathS.SpherToCart(new Vector3(p.x,ThetaRad,PhiRad));
                break;
            case InputType.Cartesian:
                Vector3 rad = MathS.CartToSpher(p);
                R = rad.x;
                ThetaRad = rad.y;
                PhiRad = rad.z;
                point = p;
                break;
        }
    }
    public PointC(PointC p)
    {
        R = p.R;
        ThetaRad = p.ThetaRad;
        PhiRad = p.PhiRad;
        point = p.point;
    }
}