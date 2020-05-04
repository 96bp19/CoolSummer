using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FruitsChecker : MonoBehaviour
{
   
    int currentFruitsCount =0;

    bool allowedFruitMovingOutsideOfBoard = false;

    List<Color> addedColors;

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
        addedColors = new List<Color>();
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
            Color fruitcolor = other.GetComponent<Fruit>().fruitColor;
            if (!addedColors.Contains(fruitcolor))
            {
                addedColors.Add(fruitcolor);
            }
            if (currentFruitsCount == 0)
            {
                GameSequencer.Instance.mixedColor = ColorConverter.getMixedColor(addedColors.ToArray());
                GameSequencer.Instance.OnItemDragFinish();
            }

        }
    }



  

   

}
