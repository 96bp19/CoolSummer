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

    public void MakePopsicleLayer(FruitsInfoHolder fruitinfo , int[] fruitaccessIndex )
    {
        mat.SetColor(buttomcolor, fruitinfo.allfruits[fruitaccessIndex[0]].fruitLiquidcolor);
        mat.SetColor(middlecolor, fruitinfo.allfruits[fruitaccessIndex[1]].fruitLiquidcolor);
        mat.SetColor(topcolor, fruitinfo.allfruits[fruitaccessIndex[2]].fruitLiquidcolor);
       
    }




}
