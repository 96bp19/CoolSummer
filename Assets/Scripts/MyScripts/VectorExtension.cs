using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension 
{
    public static void SetPosition(this Transform transform, float x = float.NaN, float y = float.NaN, float z = float.NaN)
    {
        var pos = transform.position;
        if (!float.IsNaN(x))
            pos.x = x;
        if (!float.IsNaN(y))
            pos.y = y;
        if (!float.IsNaN(z))
            pos.z = z;

        transform.position = pos;
    }
}
