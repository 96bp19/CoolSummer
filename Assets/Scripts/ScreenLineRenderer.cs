using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLineRenderer : MonoBehaviour {

    // Line Drawn event handler
    public delegate void LineDrawnHandler(Vector3 begin, Vector3 end, Vector3 depth);
    public event LineDrawnHandler OnLineDrawn;
    public SlicedObjectCounter objectCounter;
    

    bool dragging;
    Vector3 start;
    Vector3 end;
    Camera cam;

    public Material lineMaterial;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        dragging = false;
        GameSequencer.GameInitializeListeners += OnGameInitialized;
    }

    void OnGameInitialized(int val)
    {
        allowCutting = true;
    }

    private void OnEnable()
    {
        Camera.onPostRender += PostRenderDrawLine;
    }

    private void OnDisable()
    {
        Camera.onPostRender -= PostRenderDrawLine;
    }

    // Update is called once per frame
    void Update () {
        //old style
        //DragStyleCutting();


        // new style
         CutOnClick();
        //CutOnClick_02();

    }

    void CutOnClick_02()
    {
        if (allowCutting == false) return;
        if (Input.GetMouseButtonDown(0))
        {

            start = bladeStartpoint.transform.position;
            end = bladeEndPoint.transform.position;



            Debug.DrawLine(start, end, Color.black);

            dragging = false;

            var startRay = cam.ViewportPointToRay(start);
            var endRay = cam.ViewportPointToRay(end);

            Debug.DrawLine(startRay.origin, startRay.direction * 3);
            Debug.DrawLine(endRay.origin, endRay.direction * 3);

            // Raise OnLineDrawnEvent
            if (objectCounter.CountSlicedObjects() < 50)
            {
                OnLineDrawn?.Invoke(start,end,Vector3.right);

            }
            else
            {
                allowCutting = false;
                Debug.Log("limit reached");
                GameSequencer.Instance.OnItemCutFinish();
            }
        }
    }

    void CutOnClick()
    {
        if (allowCutting == false) return;
        if (Input.GetMouseButtonDown(0))
        {

            start = cam.WorldToViewportPoint(bladeStartpoint.transform.position);
            end = cam.WorldToViewportPoint(bladeEndPoint.transform.position);

           

            Debug.DrawLine(start, end, Color.black);
          
            dragging = false;

            var startRay = cam.ViewportPointToRay(start);
            var endRay = cam.ViewportPointToRay(end);

            Debug.DrawLine(startRay.origin, startRay.direction * 3);
            Debug.DrawLine(endRay.origin, endRay.direction * 3);

            // Raise OnLineDrawnEvent
            if (objectCounter.CountSlicedObjects() < 50)
            {
                OnLineDrawn?.Invoke(
                    startRay.GetPoint(cam.nearClipPlane),
                    endRay.GetPoint(cam.nearClipPlane),
                    endRay.direction.normalized);

            }
            else
            {
                allowCutting = false;
                Debug.Log("limit reached");
                GameSequencer.Instance.OnItemCutFinish();
            }
        }
    }
    public GameObject bladeStartpoint, bladeEndPoint;
    bool allowCutting = true;
    void DragStyleCutting()
    {
        if (allowCutting == false)
        {
            return;
        }
        if (!dragging && Input.GetMouseButtonDown(0))
        {
            start = cam.ScreenToViewportPoint(Input.mousePosition);
            dragging = true;
        }

        if (dragging)
        {
            end = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if ( Input.GetMouseButtonDown(0))
        {
            // Finished dragging. We draw the line segment
         
            
            end = cam.ScreenToViewportPoint(Input.mousePosition);
            dragging = false;

            var startRay = cam.ViewportPointToRay(start);
            var endRay = cam.ViewportPointToRay(end);

            // Raise OnLineDrawnEvent
            if (objectCounter.CountSlicedObjects() < 50)
            {
                OnLineDrawn?.Invoke(
                    startRay.GetPoint(cam.nearClipPlane),
                    endRay.GetPoint(cam.nearClipPlane),
                    endRay.direction.normalized);


            }
            else
            {
                allowCutting = false;
                Debug.Log("limit reached");
                GameSequencer.Instance.OnItemCutFinish();
            }
        }
    }
    

    /// <summary>
    /// Draws the line in viewport space using start and end variables
    /// </summary>
    private void PostRenderDrawLine(Camera cam)
    {
        if (dragging && lineMaterial)
        {
            GL.PushMatrix();
            lineMaterial.SetPass(0);
            GL.LoadOrtho();
            GL.Begin(GL.LINES);
            GL.Color(Color.black);
            GL.Vertex(start);
            GL.Vertex(end);
            GL.End();
            GL.PopMatrix();
        }
    }
}
