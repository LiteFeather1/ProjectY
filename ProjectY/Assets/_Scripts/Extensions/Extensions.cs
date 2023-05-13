using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Ray SetOriginAndDirection(this Ray ray, Vector3 origin, Vector3 direction)
    {
        ray.origin = origin;
        ray.direction = direction;
        return ray;
    }

    public static void ChangeY(this Vector3 v3, float y)
    {
        v3 = new(v3.x, y, v3.z);
    }
}
