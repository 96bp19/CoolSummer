using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyGlassSeperator : MonoBehaviour
{
    public ClipPlane emptyflaskliquidFiller;
    private void Awake()
    {
        emptyflaskliquidFiller.enabled = false;
        GameSequencer.ItemPourStartListener += OnItemPourStart;
        GameSequencer.GameInitializeListeners += OnGameInitialized;
        
    }

    void OnGameInitialized(int val)
    {
        emptyflaskliquidFiller.enabled = false;
    }

    void OnItemPourStart()
    {
        emptyflaskliquidFiller.transform.localPosition = new Vector3(0, -0.8f, 0);
        emptyflaskliquidFiller.enabled = true;

    }
}
