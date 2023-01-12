using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHelper
{
    public static bool CompareRotations(Quaternion quatA, Quaternion quatB, float toleranceRange)
    {
        return Quaternion.Angle(quatA, quatB) < toleranceRange;
    }
    /// <summary>
    /// Returns the angle between two points
    /// </summary>
    public static float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    /// <summary>
    /// Map a value from a range to another
    /// </summary>
    public static float Map(float value, float min1, float max1, float min2, float max2, bool clamp = false)
    {
        float val = min1 + (max1 - min1) * ((value - min2) / (max2 - min2));

        return clamp ? Mathf.Clamp(val, Mathf.Min(min2, max2), Mathf.Max(min2, max2)) : val; 
    }

    public static float RandomFromVec2(Vector2 vector2)
    {
        return Random.Range(vector2.x, vector2.y);
    }

    public static Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
        float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
        return new Vector2(_x, _y);
    }
}
