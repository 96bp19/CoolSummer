using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedObjectCounter : MonoBehaviour
{


    private void Awake()
    {
        GameSequencer.ItemMoveStartListener += MoveObjectToMixer;
        GameSequencer.ItemMixStartListener += OnMixStarted;
    }

    Rigidbody[] slicedfruits;
    void MoveObjectToMixer()
    {
      
        MoveToMixer();
        Invoke("OnMoveComplete", 2.5f);
    }

    
    void OnMixStarted()
    {
        StartCoroutine(startmixing());
      
    }

    public void removeLeftoverFruits()
    {
        foreach (var fruit in slicedfruits)
        {                  
        }
    }

    IEnumerator startmixing()
    {
        foreach (var fruit in slicedfruits)
        {
            yield return null;
           Destroy(fruit.gameObject);            

           
        }

      

    }


    public int CountSlicedObjects()
    {
        return transform.childCount;
    }


    void MoveToMixer()
    {
        SetKinematicToAllCutFruits(false);


    }

    public void SetKinematicToAllCutFruits(bool val)
    {
        slicedfruits = transform.GetComponentsInChildren<Rigidbody>();
        foreach (var item in slicedfruits)
        {
            item.isKinematic = val;
        }
    }

  

    



}
