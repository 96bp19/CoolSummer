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
        ObjectDragger.fruitDragListener += OnFruitInsideChoppingBoard;
        
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
    

    
    public void OnFruitInsideChoppingBoard( Fruit fruit)
    {
        if (currentFruitsCount == 0) return;
        if (fruit.gameObject.CompareTag("Fruits"))
        {
            fruit.gameObject.tag = "Sliceable";
            fruit.transform.SetParent(transform);
            currentFruitsCount--;
            Debug.Log("current fruit count :" + currentFruitsCount);
            Color fruitcolor = fruit.fruitColor;
            if (!addedColors.Contains(fruitcolor))
            {
                addedColors.Add(fruitcolor);
            }
            if (currentFruitsCount == 0)
            {
                GameSequencer.Instance.playerCutFruits.Add(fruit.fruitIndex);
                GameSequencer.Instance.mixedColor = ColorConverter.getMixedColor(addedColors.ToArray());
                GameSequencer.Instance.OnItemDragFinish();
            }

        }
    }

 



  

   

}
