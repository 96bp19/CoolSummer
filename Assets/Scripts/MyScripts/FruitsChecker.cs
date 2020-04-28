using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FruitsChecker : MonoBehaviour
{
   
    int currentFruitsCount =0;

    bool allowedFruitMovingOutsideOfBoard = false;

    private void Awake()
    {
        GameSequencer.GameInitializeListeners += OnGameInitialized;
        GameSequencer.ItemCutCompleteListener += OnObjectMoveAllowed;
        
    }

    void OnObjectMoveAllowed()
    {
        allowedFruitMovingOutsideOfBoard = true;
    }

  

    void OnGameInitialized( int NoOfFruitsToDrag)
    {
        allowedFruitMovingOutsideOfBoard = false;
        currentFruitsCount = NoOfFruitsToDrag;
        Debug.Log("current fruit count initialized:" + currentFruitsCount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentFruitsCount == 0) return;
        if (other.gameObject.CompareTag("Fruits"))
        {
            other.gameObject.tag = "Sliceable";
            other.transform.SetParent(transform);
            currentFruitsCount--;
            Debug.Log("current fruit count :" + currentFruitsCount);
            if (currentFruitsCount == 0)
            {
                GameSequencer.Instance.OnItemDragFinish();
            }

        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sliceable") && !allowedFruitMovingOutsideOfBoard)
        {
          //  other.transform.position = transform.position + Vector3.up;


        }
    }

   

}
