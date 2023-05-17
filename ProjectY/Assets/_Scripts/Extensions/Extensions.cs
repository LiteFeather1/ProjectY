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
}
