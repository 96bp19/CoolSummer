using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDragger : MonoBehaviour
{
    public LayerMask DragableLayer;
    public LayerMask ChoppingBoardLayer;

    Transform currentSelectedObj;
    Vector3 selectedObjStartPos;
    Quaternion selectedObjRotation;

    bool draggingObj;
    Camera mainCam;

   
    public GameObject Knife;
    public Vector2 knifeClampMin, knifeClampMax;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        Ray ray =mainCam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.black);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, DragableLayer))
            {
                if (!hitInfo.transform.gameObject.CompareTag("Sliceable"))
                {
                    currentSelectedObj = hitInfo.transform;
                    selectedObjRotation = currentSelectedObj.rotation;
                    selectedObjStartPos = currentSelectedObj.position;
                    draggingObj = true;
                   

                }
            }

        }

        if (Input.GetMouseButtonUp(0) && currentSelectedObj !=null)
        {
            OnObjectDropped();
            draggingObj = false;
            currentSelectedObj = null;

        }

        if (draggingObj)
        {
            DragObject(currentSelectedObj.gameObject);
        }

        DragObject(Knife);
        LimitKnifeMovement();
       


    }

    void LimitKnifeMovement()
    {
        Vector3 newpos = Knife.transform.position;
        newpos.y = 0f;
        newpos.x = Mathf.Clamp(newpos.x,knifeClampMin.x, knifeClampMax.x);
        newpos.z = Mathf.Clamp(newpos.z, knifeClampMin.y, knifeClampMax.y);
        Knife.transform.position = newpos;
    }


    void DragObject( GameObject objectToDrag)
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = mainCam.WorldToScreenPoint(objectToDrag.transform.position).z;
        Vector3 movePos  = mainCam.ScreenToWorldPoint(screenPoint);
        movePos.y = objectToDrag.transform.position.y;
        objectToDrag.transform.position = movePos;

    }

    void OnObjectDropped()
    {
       var colliders = Physics.OverlapSphere(currentSelectedObj.position, 0.1f);
        foreach (var item in colliders)
        {
            if (item.CompareTag("ChoppingBoard"))
            {
                Debug.Log("chopping board found");
                StartCoroutine(SetItemToSliceable(currentSelectedObj.gameObject));
                break;
            }
        }
    }

   IEnumerator SetItemToSliceable( GameObject obj)
    {
        int frametoskip = 3;
        while(frametoskip>0)
        {
            frametoskip--;
            yield return null;
        }
        obj.tag = "Sliceable";
    }
}
