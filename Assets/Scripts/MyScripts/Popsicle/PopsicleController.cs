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
        
        mat.SetColor(buttomcolor, GameSequencer.Instance.fruitinfoHolder.allfruits[GameSequencer.Instance.playerCutFruits[0]].fruitLiquidcolor);
        mat.SetColor(middlecolor, GameSequencer.Instance.fruitinfoHolder.allfruits[GameSequencer.Instance.playerCutFruits[1]].fruitLiquidcolor);
        mat.SetColor(topcolor, GameSequencer.Instance.fruitinfoHolder.allfruits[GameSequencer.Instance.playerCutFruits[2]].fruitLiquidcolor);
    }


}
