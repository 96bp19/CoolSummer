using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fruit : MonoBehaviour
{
    public Color fruitColor;
    public Color fruitLiquidcolor;
    public bool fruitUnlocked;
    [HideInInspector]
    public int fruitIndex;
    private void Awake()
    {
        fruitColor.a = 1;
        fruitLiquidcolor.a = 1;
    }

  
}
