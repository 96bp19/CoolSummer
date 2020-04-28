using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FruitsChecker : MonoBehaviour
{
    public SlicedObjectCounter counter;
    int currentFruitsCount =0;

    private void Awake()
    {
        GameSequencer.GameInitializeListeners += OnGameInitialized;
        
    }

  

    void OnGameInitialized( int NoOfFruitsToDrag)
    {
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
        if (other.CompareTag("Sliceable"))
        {
           // StartCoroutine(destroyAfterSec(other.gameObject));

        }
    }

    IEnumerator destroyAfterSec(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        if (obj)
        {
            Destroy(obj);
        }
    }

}
