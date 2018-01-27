using UnityEngine;
public static class Converter
{
    public static Vector3 CartesianToSpherical(Vector3 cartesianCoordinates)
    {
        float x = cartesianCoordinates.x;
        float y = cartesianCoordinates.y;
        float z = cartesianCoordinates.z;
        float r = Mathf.Sqrt(x * x + y * y + z * z);
        float theta = Mathf.Asin(y / r);
        float phi = Mathf.Atan2(z, x);
        return new Vector3(r, theta, phi < 0 ? Mathf.PI * 2 - Mathf.Abs(phi)  : phi);
    }
    public static Vector3 SphericalToCartesian(Vector3 sphericalCoordinates)
    {
        float r = sphericalCoordinates.x;
        float theta = sphericalCoordinates.y;
        float phi = sphericalCoordinates.z;
        float x = r * Mathf.Cos(theta) * Mathf.Cos(phi);
        float z = r * Mathf.Cos(theta) * Mathf.Sin(phi);
        float y = r * Mathf.Sin(theta);
        return new Vector3(x, y, z);
    }
}
