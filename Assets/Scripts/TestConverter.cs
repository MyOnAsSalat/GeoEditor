using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TestConverter
{
    /* returns arcsin in deg in range [-90,90] */
    public static float degAsin(float z, float r)
    {
        float f = Mathf.Rad2Deg * Mathf.Asin(z/ r);
        return f;
    }
    /* returns arctan in deg in range [0,360) */
    public static float degAtan(float y, float x)
    {
        float f = Mathf.Rad2Deg * Mathf.Atan2(y, x);
        while (f < 0) f += 360;
        while (f >= 360) f -= 360;
        return f;
    }

    public static Vector3 CartesianToSpherical(Vector3 cartesianCoordinates)
    {
        float x = cartesianCoordinates.x;
        float y = cartesianCoordinates.y;
        float z = cartesianCoordinates.z;
        float r = Mathf.Sqrt(x * x + y * y + z * z);
        float theta = degAsin(z, r);
        float phi = degAtan(y, x);
        return new Vector3(r, theta, phi);
    }

    public static Vector3 SphericalToCartesian(Vector3 sphericalCoordinates)
    {
        float r = sphericalCoordinates.x;
        float theta = Mathf.Deg2Rad * sphericalCoordinates.y;
        float phi = Mathf.Deg2Rad * sphericalCoordinates.z;
        return new Vector3(r * Mathf.Sin(theta) * Mathf.Cos(phi), r * Mathf.Sin(theta) * Mathf.Sin(phi), r * Mathf.Cos(theta));
    }

}
