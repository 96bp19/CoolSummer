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

    string DragedLayer = "DraggedObject" , fruitLayer;

    public delegate void OnFruitDraggedToChoppingBoard(Fruit fruit);
    public static OnFruitDraggedToChoppingBoard fruitDragListener;

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
                if (hitInfo.transform.gameObject.CompareTag("Dragable"))
                {
                    currentSelectedObj = hitInfo.transform;
                    selectedObjRotation = currentSelectedObj.rotation;
                    selectedObjStartPos = currentSelectedObj.position;
                    draggingObj = true;
                    fruitLayer = LayerMask.LayerToName(currentSelectedObj.gameObject.layer);
                    ChangeDragableObjectProperty();
                 
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
            DragObject(currentSelectedObj.gameObject,false);
        }

        DragObject(Knife,true);
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


    void DragObject( GameObject objectToDrag, bool isknife)
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = mainCam.WorldToScreenPoint(objectToDrag.transform.position).z;
        Vector3 movePos  = mainCam.ScreenToWorldPoint(screenPoint);
        if (isknife == false)
        {
            movePos.y = selectedObjStartPos.y + 1f;

        }
        else
        movePos.y = objectToDrag.transform.position.y;
        objectToDrag.transform.position = movePos;

    }

  

    void ChangeDragableObjectProperty()
    {
        currentSelectedObj.GetComponent<Rigidbody>().isKinematic = true;
        currentSelectedObj.gameObject.layer = LayerMask.NameToLayer(DragedLayer);
    }

    void ReturnToOriginalPos()
    {
        currentSelectedObj.GetComponent<Rigidbody>().isKinematic = false;
        currentSelectedObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        currentSelectedObj.gameObject.layer = LayerMask.NameToLayer(fruitLayer);
        currentSelectedObj.gameObject.tag = "Dragable";
        currentSelectedObj.transform.position = selectedObjStartPos;
        currentSelectedObj.transform.rotation = selectedObjRotation;
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
                return;
            }
        }
        Debug.Log("no choping board found");
        ReturnToOriginalPos();
    }

   IEnumerator SetItemToSliceable( GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(1f);
        fruitDragListener?.Invoke(obj.GetComponent<Fruit>());
        
    }
}
