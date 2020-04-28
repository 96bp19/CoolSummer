﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSequencer: MonoBehaviour
{

    public static GameSequencer Instance;
    public int noOFFruitsToDrag =3;
    public delegate void OnGameStarted(int noOfFruitsToDrag);
    public static OnGameStarted GameInitializeListeners;

    
    public Color mixedColor;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnGameInitialized();
    }

    void OnGameInitialized()
    {
        GameInitializeListeners?.Invoke(noOFFruitsToDrag);
        // start of the game 
        // show items to cut
    }

    public delegate void OnItemDragComplete();
    public static OnItemDragComplete ItemDragCompleteListener;

     public void OnItemDragFinish()
    {
        ItemDragCompleteListener?.Invoke();
    }


    public delegate void OnItemCutComplete();
    public static OnItemCutComplete ItemCutCompleteListener;
    public void OnItemCutFinish()
    {
        ItemCutCompleteListener?.Invoke();
    }

    public delegate void OnItemMoveStart();
    public static OnItemMoveStart ItemMoveStartListener;
    public void OnItemMoveStartToBlender()
    {
        ItemMoveStartListener?.Invoke();
    }

    public delegate void OnItemMoveComplete();
    public static OnItemMoveComplete ItemMoveCompleteListener;
    public void OnItemMovedToBlender()
    {
        ItemMoveCompleteListener?.Invoke();
    }

    public delegate void OnItemMixStart();
    public static OnItemMixStart ItemMixStartListener;
    public void OnItemMixStarted()
    {
        ItemMixStartListener?.Invoke();
        Invoke("OnItemMixFinish", 3f);
    }

    public delegate void OnItemMixComplete();
    public static OnItemMixComplete ItemMixCompleteListener;
    public void OnItemMixFinish()
    {
        ItemMixCompleteListener?.Invoke();
    }


    public delegate void OnItemPourStart();
    public static OnItemPourStart ItemPourStartListener;
    public void OnItemPourStarted()
    {
        ItemPourStartListener?.Invoke();
    }

    public delegate void OnItemPourComplete();
    public static OnItemPourComplete ItemPourCompleteListener;
    public void OnItemPoured()
    {
        ItemPourCompleteListener?.Invoke();
    }


   
    
}