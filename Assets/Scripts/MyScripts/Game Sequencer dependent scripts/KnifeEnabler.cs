using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeEnabler : MonoBehaviour
{

    public MouseSlice mouseSlice;
    public GameObject Knife;

  

    private void Awake()
    {
        GameSequencer.GameInitializeListeners += OnGameInitialized;
        GameSequencer.ItemDragCompleteListener += OnItemDragComplete;
        
    }

    private void Start()
    {
        
    }


    void OnGameInitialized(int val)
    {
        SetComponentsActive(false);
    }

    void OnItemDragComplete()
    {
        SetComponentsActive(true);
    }

    void SetComponentsActive(bool val)
    {
        mouseSlice.gameObject.SetActive(val);
        Knife.SetActive(val);
    }

  
}
