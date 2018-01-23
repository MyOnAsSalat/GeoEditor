using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SphericalConverter
{
    public static Vector3 CartesianToSpherical(Vector3 cartesianCoordinates)
    {
        float x = cartesianCoordinates.x;
        float y = cartesianCoordinates.y;
        float z = cartesianCoordinates.z;
        return new Vector3(Mathf.Sqrt(x*x+y*y+z*z),Mathf.Acos(z/Mathf.Sqrt(x*x+y*y+z*z)),  Mathf.Atan(y / x));
    }
    public static Vector3 SphericalToCartesian(Vector3 sphericalCoordinates)
    {
        float r = sphericalCoordinates.x;
        float theta = sphericalCoordinates.y;
        float phi = sphericalCoordinates.z;
        return new Vector3(r * Mathf.Sin(theta) * Mathf.Cos(phi), r * Mathf.Sin(theta) * Mathf.Sin(phi), r * Mathf.Cos(theta));
    }

}
