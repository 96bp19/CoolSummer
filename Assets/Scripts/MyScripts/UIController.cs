using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public GameObject  itemDragText, itemCutText, itemMixText, itempourText;
    public GameObject MoveToMixerButtton, MixButton, PourButton;

    public Text fruitMixOrder;


    private void Awake()
    {
        GameSequencer.GameInitializeListeners += OnGameStarted;
        GameSequencer.ItemDragCompleteListener += OnItemDragged;
        GameSequencer.ItemCutCompleteListener += OnItemCut;
        GameSequencer.ItemMoveCompleteListener += OnItemMoved;
        GameSequencer.ItemMixCompleteListener += OnItemMixed;
        GameSequencer.ItemPourCompleteListener += OnItemPoured;
    }

    void OnGameStarted(int val)
    {
        DisableAllUIObj();
        itemDragText.SetActive(true);
        string fruitTomake = "";
        var fruitcutorder = GameSequencer.Instance.blenderfruit;
        for (int i = 0; i < fruitcutorder.Count; i++)
        {
           
            fruitTomake += "  " + GameSequencer.Instance.fruitinfoHolder.allfruits[fruitcutorder[i]].name;
        }
        fruitMixOrder.text = fruitTomake;
    }

    void OnItemDragged()
    {
        DisableAllUIObj();
        itemCutText.SetActive(true);
    }

    void OnItemCut()
    {
        DisableAllUIObj();
        MoveToMixerButtton.SetActive(true);
        
    }

    void OnItemMoved()
    {
        DisableAllUIObj();
        itemMixText.SetActive(true);
        MixButton.SetActive(true);
    }

    void OnItemMixed()
    {
        DisableAllUIObj();
        itempourText.SetActive(true);
        PourButton.SetActive(true);
    }

    void OnItemPoured()
    {

    }

    void DisableAllUIObj()
    {
        
        itemDragText.SetActive(false);
        itemCutText.SetActive(false);
        itemMixText.SetActive(false);
        itempourText.SetActive(false);
        MoveToMixerButtton.SetActive(false);
        MixButton.SetActive(false);
        PourButton.SetActive(false);
    }
}
