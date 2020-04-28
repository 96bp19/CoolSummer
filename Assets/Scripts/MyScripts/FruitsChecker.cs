﻿using System.Collections;
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

    public  Color getMixedColor()
    {
        Color color = Color.black;
        color.a = 0;
        foreach (var item in addedColors)
        {
            color += item;
        }
        color /= addedColors.Count;
        return color;
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
            Color fruitcolor = other.GetComponent<Renderer>().material.color;
            if (!addedColors.Contains(fruitcolor))
            {
                addedColors.Add(fruitcolor);
            }
            if (currentFruitsCount == 0)
            {
                GameSequencer.Instance.mixedColor = getMixedColor();
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
