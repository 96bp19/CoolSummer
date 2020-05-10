using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="FruitSlotInfo",menuName ="FruitsInformation/fruitSlotInfo")]
public class FruitSlotInfo : ScriptableObject
{
    // spacing of the generated fruit
    public Vector3 fruitSpacing;

    // no of fruits generated in x , y ,z space
    public Vector3Int noOfFruits;

    // amount of fruit that needs to be draged to start cutting
    public int noOfFruitToDrag;

    public Fruit fruitPrefab;
}
