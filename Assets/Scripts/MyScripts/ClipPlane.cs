using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipPlane : MonoBehaviour
{
    public Material mat;

    private void Start()
    {
        
    }

    void Update()
    {
        //create plane
        Plane plane = new Plane(transform.up, transform.position);
        //transfer values from plane to vector4
        Vector4 planeRepresentation = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);

        Debug.Log("plane distance : " + plane.distance);
        //pass vector to shader
        mat.SetVector("_Plane", planeRepresentation);
    }
}
