using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitColorSpecifier : MonoBehaviour
{
    public Color MyColor;

    private void Start()
    {
        GetComponent<Renderer>().material.color = MyColor;
    }
}
