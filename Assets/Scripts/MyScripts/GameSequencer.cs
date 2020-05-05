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


    //  fruits that needs to be cut in order to complete the given level
    List<int> blenderfruit;
    // this is the fruits that player cut 
    public List<int> playerCutFruits;


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
        int maxfruitlength = fruitinfoHolder.allfruits.Length;
        int index = 0;
        List<int> fruitToCut = new List<int>();
        
        for (int i = 0; i < val; i++)
        {
            index = Random.Range(0, maxfruitlength);
            Debug.Log("fruit to blend order : " + fruitinfoHolder.allfruits[index]);
            Transform obj = Instantiate(fruitinfoHolder.allfruits[index]).transform;
            obj.SetParent(FruitBasket.transform);
            obj.localPosition = new Vector3((-1.5f) + 0.5f * i, 0, 0);
            obj.GetComponent<Fruit>().fruitIndex = index;
            fruitToCut.Add(index);
            blenderfruit.Add(index);
       
        }
        for (int i = val; i < 3; i++)
        {
            index = Random.Range(0, fruitToCut.Count);
            Debug.Log("fruit to blend order : " + fruitinfoHolder.allfruits[fruitToCut[index]]);
            Transform obj = Instantiate(fruitinfoHolder.allfruits[fruitToCut[index]]).transform;
            obj.GetComponent<Fruit>().fruitIndex = fruitToCut[index];
            obj.SetParent(FruitBasket.transform);
            obj.localPosition = new Vector3((-1.5f+val*0.5f) + 0.5f * i, 0, 0);
            blenderfruit.Add(fruitToCut[index]);
        }
        fruitToCut.Clear();
        
    }

    void OnGameInitialized()
    {
        if (gameJustStarted )
        {
            currentFruitBlendedCount = 0;
            fruitmixlayer = Random.Range(1, 4);
            
            gameJustStarted = false;
            ListFruitsToBlend(fruitmixlayer);

           
        }
        //         noOFFruitsToDrag = 3 - fruitmixlayer;
        //         noOFFruitsToDrag = Mathf.Clamp(noOFFruitsToDrag, 1, 3);
        noOFFruitsToDrag = 1;
        
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


        if (playerCutFruits.Count == blenderfruit.Count)
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
