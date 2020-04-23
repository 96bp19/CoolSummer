using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedObjectCounter : MonoBehaviour
{
   public int CountSlicedObjects()
   {
        return transform.childCount;
   }
}
