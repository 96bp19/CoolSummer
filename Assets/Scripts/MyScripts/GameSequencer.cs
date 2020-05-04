using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSequencer: MonoBehaviour
{

    public static GameSequencer Instance;
    public int noOFFruitsToDrag =2;
    public delegate void OnGameStarted(int noOfFruitsToDrag);
    public static OnGameStarted GameInitializeListeners;


    // holds info about various fruits used in game
    public FruitsInfoHolder fruitinfoHolder;

    // no of types of fruits to be blended
    int fruitmixlayer;
    // no of fruits blended in current level
    int currentFruitBlendedCount;

    bool gameJustStarted = true;


    
    public Color mixedColor;


    List<Fruit> blenderfruit;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnGameInitialized();
     
    }

    void ListFruitsToBlend(int val)
    {
        int maxfruitlength = fruitinfoHolder.allfruits.Length;
        for (int i = 0; i < val; i++)
        {
            Debug.Log("fruit to blend order : " + fruitinfoHolder.allfruits[Random.Range(0, maxfruitlength - 1)]);
        }
        blenderfruit = new List<Fruit>();
        
    }

    void OnGameInitialized()
    {
        if (gameJustStarted )
        {
            currentFruitBlendedCount = 0;
            fruitmixlayer = Random.Range(1, 4);
            gameJustStarted = true;
            ListFruitsToBlend(fruitmixlayer);

           
        }
        noOFFruitsToDrag = 3 - fruitmixlayer;
        noOFFruitsToDrag = Mathf.Clamp(noOFFruitsToDrag, 1, 3);
        
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
        OnFreezeStart();
    }

    public delegate void OnItemfreezeStart();
    public static OnItemfreezeStart ItemfreezeStartListener;
    public void OnFreezeStart()
    {
        Debug.Log("item freeze start");
       ItemfreezeStartListener?.Invoke();

        checkLevelCompleteStatus();
    }
    

    public delegate void OnItemfreezeComplete();
    public static OnItemfreezeComplete ItemfreezeCompleteListener;
    public void OnItemfreezed()
    {
        ItemfreezeCompleteListener?.Invoke();
        checkLevelCompleteStatus();
    }

    

    void checkLevelCompleteStatus()
    {
        currentFruitBlendedCount++;

        if (currentFruitBlendedCount == fruitmixlayer)
        {
            gameJustStarted = true;
            OnLevelComplete();

        }
        else
        {
            OnGameInitialized();
        }
    }

  
    public delegate void OnlevelFinish();
    public static OnlevelFinish levelCompleteListener;
    public void OnLevelComplete()
    {
        levelCompleteListener?.Invoke();
        
        Invoke("RestartGameLoop", 2f);

    }

    void RestartGameLoop()
    {
        OnGameInitialized();
    }


}
