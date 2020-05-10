using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopsicleController : MonoBehaviour
{

    string topcolor="_TopColor", middlecolor = "_MiddleColor", buttomcolor ="_ButtomColor";
    Material mat;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        GameSequencer.levelCompleteListener += OnPopsicleMade;
    }

    void OnPopsicleMade()
    {

        MakePopsicleLayer(GameSequencer.Instance.fruitinfoHolder, GameSequencer.Instance.playerCutFruits.ToArray());
    }

    public void MakePopsicleLayer(FruitSlotManager fruitinfo , int[] fruitaccessIndex )
    {
        mat.SetColor(buttomcolor, fruitinfo.fruitslots[fruitaccessIndex[0]].fruitSlotInfo.fruitPrefab.fruitLiquidcolor);
        mat.SetColor(middlecolor, fruitinfo.fruitslots[fruitaccessIndex[1]].fruitSlotInfo.fruitPrefab.fruitLiquidcolor);
        mat.SetColor(topcolor, fruitinfo.fruitslots[fruitaccessIndex[2]].fruitSlotInfo.fruitPrefab.fruitLiquidcolor);
       
       
    }




}
