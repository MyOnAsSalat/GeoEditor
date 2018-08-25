using System.Collections.Generic;
using UnityEngine;
public static class MathS
{
    //Перевод из декартовых в сферические, при условии что центр сферы в 0,0,0
    //z - y изменены для координат unity
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
    //Перевод из сферических к декартовым: радиус, тета, фи
    //z - y изменены для координат unity
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
    //Угловое расстояние между точками в радианах
    public static float ArcLengthRad(PointC p1, PointC p2)
    {
        return Mathf.Acos(Mathf.Sin(p1.ThetaRad) * Mathf.Sin(p2.ThetaRad) +  Mathf.Cos(p1.ThetaRad) * Mathf.Cos(p2.ThetaRad) * Mathf.Cos(p1.PhiRad - p2.PhiRad));
    }
    //Угловое расстояние между точками в градусах
    public static float ArcLength(PointC p1, PointC p2)
    {
        return ArcLengthRad(p1, p2) * Mathf.Rad2Deg;
    }
    //Угол по трём точкам
    public static float AngleByPoint(PointC p1, PointC vertex, PointC p2)
    {
        float a = ArcLengthRad(vertex, p1);
        float b = ArcLengthRad(vertex, p2);
        float c = ArcLengthRad(p1, p2);
        return  Mathf.Acos((Mathf.Cos(b) - Mathf.Cos(c) * Mathf.Cos(a)) / (Mathf.Sin(c) * Mathf.Sin(a)));
    }
    //Сферический эксцесс для треугольника
    public static float TriangleExcess(PointC p1, PointC p2, PointC p3)
    {
        float A = AngleByPoint(p1, p2, p3);
        float B = AngleByPoint(p3, p1, p2);
        float C = AngleByPoint(p2, p3, p1);
        return A + B + C - Mathf.PI;
    }
    //Площадь треугольника через сферический эксцесс
    public static float TriangleArea(PointC p1, PointC p2, PointC p3)
    {
        return TriangleExcess(p1, p2, p3) * p1.R;
    }

   
    //Получение промежуточных точек кратчайшего расстояния между двумя точками на сфере в Unity
    public static Vector3[] GetIntermeditatePoints(PointC point1, PointC point2,int segmentCount)
    {
        var center = Vector3.zero;
        var p1 = point1.Point - center;
        var p2 = point2.Point - center;
        var radius = p2.magnitude + 0.01f;

        var points = new Vector3[segmentCount + 1];

        for (int i = 0; i < segmentCount + 1; i++)
        {
            var k = (float)i / segmentCount;
            var p = Vector3.Slerp(p1, p2, k);
            p = p.normalized * radius;
            points[i] = center + p;
        }
        return points;
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
    public readonly Vector3 Point;

    public PointC(float _phiRad, float _thetaRad,float _r = 5)
    {
        PhiRad = _phiRad;
        ThetaRad = _thetaRad;
        R = _r;
        Point = MathS.SpherToCart(new Vector3(R,ThetaRad,PhiRad));
    }
    public PointC(Vector3 p,InputType type)
    {
        switch (type)
        {
            case InputType.Radians:
                R = p.x;
                ThetaRad = p.y;
                PhiRad = p.z;
                Point = MathS.SpherToCart(p);
                break;
            case InputType.Degrees:
                R = p.x;
                ThetaRad = p.y * Mathf.Deg2Rad;
                PhiRad = p.z * Mathf.Deg2Rad;
                Point = MathS.SpherToCart(new Vector3(R,ThetaRad,PhiRad));
                break;
            case InputType.Cartesian:
                Vector3 rad = MathS.CartToSpher(p);
                R = rad.x;
                ThetaRad = rad.y;
                PhiRad = rad.z;
                Point = p;
                break;
        }
    }
    public PointC(PointC p)
    {
        R = p.R;
        ThetaRad = p.ThetaRad;
        PhiRad = p.PhiRad;
        Point = p.Point;
    }
}