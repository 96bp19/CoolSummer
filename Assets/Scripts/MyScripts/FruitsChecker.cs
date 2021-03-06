﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FruitsChecker : MonoBehaviour
{
   
    int currentFruitsCount =0;

    bool allowedFruitMovingOutsideOfBoard = false;

    List<Color> addedColors;
    public PhysicMaterial mat;

    [HideInInspector]
    public int draggedFruitType =-1;

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

    bool resetPhysicsMat = false;
    public void ChangePhysicsMat()
    {
        resetPhysicsMat = !resetPhysicsMat;
        if (resetPhysicsMat)
        {
            GetComponent<Collider>().material = mat;
            Debug.Log("mat changed");
        }
        else
        {
            GetComponent<Collider>().material = null;
            Debug.Log("mat reset");
        }
    }




    void OnGameInitialized(int NoOfFruitsToDrag)
    {
        addedColors = new List<Color>();
        allowedFruitMovingOutsideOfBoard = false;
        fruitStrength = 0;
        // currentFruitsCount = NoOfFruitsToDrag;
        Debug.Log("current fruit count initialized:" + currentFruitsCount);
    }


    // lower means more fruits needs to be dragged and vice versa
    [HideInInspector] public float fruitStrength =0;
    public void OnFruitInsideChoppingBoard( Fruit fruit)
    {
       
       
        if (fruitStrength >=1f) return;
        if (fruit.gameObject.CompareTag("Dragable"))
        {
            fruit.gameObject.tag = "Sliceable";
            fruit.transform.SetParent(transform);
            fruitStrength += fruit.fruitStrength;
            Debug.Log("current fruit count :" + currentFruitsCount);
            Color fruitcolor = fruit.fruitColor;
            if (!addedColors.Contains(fruitcolor))
            {
                addedColors.Add(fruitcolor);
            }
            if (fruitStrength >=1f)
            {
                draggedFruitType = -1;
                GameSequencer.Instance.playerCutFruits.Add(fruit.fruitIndex);
                GameSequencer.Instance.mixedColor = ColorConverter.getMixedColor(addedColors.ToArray());
                GameSequencer.Instance.OnItemDragFinish();
            }

        }
    }

    public bool isSameFruit( Fruit fruit)
    {
        if (draggedFruitType == -1)
        {
            draggedFruitType = fruit.fruitIndex;
        }

        return draggedFruitType == fruit.fruitIndex;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sliceable"))
        {
            other.transform.SetParent(null);
            other.gameObject.layer = LayerMask.NameToLayer("ChoppedFruits");
            
        }
    }

    [HideInInspector]
    public List<Rigidbody> allChildobj = new List<Rigidbody>();

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sliceable"))
        {
            other.transform.SetParent(transform);
            if (!allChildobj.Contains(other.GetComponent<Rigidbody>()))
            {
                allChildobj.Add(other.GetComponent<Rigidbody>());
            }
        }
    }









}
