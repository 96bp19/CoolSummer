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
    public FruitSlotManager fruitinfoHolder;

    // no of types of fruits to be blended
    int fruitmixlayer;
    // no of fruits blended in current level
    int currentFruitBlendedCount;

    bool gameJustStarted = true;


    
    [HideInInspector] public Color mixedColor;


    //  fruits that needs to be cut in order to complete the given level
    [HideInInspector] public List<int> blenderfruit;
    // this is the fruits that player cut 
    [HideInInspector] public List<int> playerCutFruits;

    List<int> noOfFruitsToDrag;


    public GameObject FruitBasket;

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
        blenderfruit = new List<int>();
        playerCutFruits = new List<int>();
        noOfFruitsToDrag = new List<int>();
        int maxfruitlength = fruitinfoHolder.fruitslots.Length;
        int index = 0;
  
        for (int i = 0; i < 3; i++)
        {
            index = Random.Range(0, maxfruitlength);
            Debug.Log("fruit to blend order : " + fruitinfoHolder.fruitslots[index].fruitSlotInfo.fruitPrefab);
            noOfFruitsToDrag.Add(fruitinfoHolder.GetNoOfFruitsToDrag(index));
            fruitinfoHolder.GenerateFruit(index);
            blenderfruit.Add(index);
       
        }
    }

    void DestroyPreviousFruits()
    {
        fruitinfoHolder.DestroyPreviousFruits();
        
    }

    void OnGameInitialized()
    {
        if (gameJustStarted )
        {
            currentFruitBlendedCount = 0;
            fruitmixlayer = Random.Range(1, 4);
            
            gameJustStarted = false;
            DestroyPreviousFruits();
            ListFruitsToBlend(fruitmixlayer);

           
        }
     
     //   noOFFruitsToDrag = noOfFruitsToDrag[currentFruitBlendedCount];
        
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


        if (currentFruitBlendedCount == 3)
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
        currentFruitBlendedCount = 0;

    }

    void RestartGameLoop()
    {
        OnGameInitialized();
    }


}
