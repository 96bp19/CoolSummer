using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractObjects : MonoBehaviour
{
    public Color col1, col2;
    public Color32 col_1, col_2;

    private void OnDrawGizmos()
    {
        Gizmos.color = ColorConverter.mixRGB_color(col1 , col2);
        Gizmos.DrawCube(transform.position, Vector3.one);

        Gizmos.color = ColorConverter.mixRGB_color32(col_1, col_2);
        Gizmos.DrawCube(transform.position + Vector3.right*2, Vector3.one);
        
    }
}
