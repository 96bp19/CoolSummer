using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabFieldManager : MonoBehaviour
{
    public Transform UpperBodyTabField, LowerBodyTabField, ShoeTabField , headField;
    public GameObject[] UpperBodyItems;
    public GameObject[] lowerBodyItems;
    public GameObject[] ShoeItems;
    public GameObject[] headItems;


    private void Start()
    {
        InstantiateMultipleObject(UpperBodyItems, UpperBodyTabField);
        InstantiateMultipleObject(lowerBodyItems,LowerBodyTabField);
        InstantiateMultipleObject(ShoeItems,ShoeTabField);
        InstantiateMultipleObject(headItems, headField);
    }

    void InstantiateMultipleObject(GameObject[] objToInstiantiate,Transform parent)
    {
        for (int i = 0; i < objToInstiantiate.Length; i++)
        {
            Instantiate(objToInstiantiate[i], parent);
        }
    }
}
