using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FruitSlotManager : MonoBehaviour
{
  //  public FruitSlotInfo[] allFruitSlots;
    //public Fruit[] allFruitSlots;
    public FruitInfo[] fruitslots;

    List<Transform> spawnedFruits;
    List<int> usedIndex;

    public void GenerateFruit(int index)
    {
        if (usedIndex.Contains(index))
        {
            return;
        }
        usedIndex.Add(index);
        Vector3 spacing = fruitslots[index].fruitSlotInfo.fruitSpacing;
        Vector3Int noofFruitToSpawn = fruitslots[index].fruitSlotInfo.noOfFruits;
        Fruit spawnPrefab = fruitslots[index].fruitSlotInfo.fruitPrefab;

        Transform spawnedObjTrans;
        for (int x = 0; x < noofFruitToSpawn.x; x++)
        {
            for (int y = 0; y < noofFruitToSpawn.y; y++)
            {
                for (int z = 0; z < noofFruitToSpawn.z; z++)
                {
                    Fruit fruit = Instantiate(spawnPrefab);
                    fruit.fruitIndex = index;
                    fruit.fruitStrength = 1f / fruitslots[index].fruitSlotInfo.noOfFruitToDrag;
                    spawnedObjTrans =fruit.transform;
                    spawnedObjTrans.SetParent(fruitslots[index].fruitSlotParent);
                    spawnedObjTrans.localPosition = new Vector3(spacing.x * x, spacing.y * y, spacing.z * z);
                    spawnedFruits.Add(spawnedObjTrans);
                }
            }
        }

    }

    public int GetNoOfFruitsToDrag(int index)
    {
        return fruitslots[index].fruitSlotInfo.noOfFruitToDrag;
    }

    public void DestroyPreviousFruits()
    {
        if (spawnedFruits == null)
        {
            spawnedFruits = new List<Transform>();
            usedIndex = new List<int>();
                Debug.Log("spawned fruit inotialized");
        }

       
        foreach (var item in spawnedFruits)
        {
            if (item != null)
            {
                Destroy(item.gameObject);
                Debug.Log("should be destroying");
            }
        }

        spawnedFruits.Clear();
        usedIndex.Clear();


    }

    [System.Serializable]
    public struct FruitInfo
    {
        public FruitSlotInfo fruitSlotInfo;
        public Transform fruitSlotParent;
    }

}





