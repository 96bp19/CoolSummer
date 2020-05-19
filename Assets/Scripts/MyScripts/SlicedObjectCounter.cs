using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(FruitsChecker))]
public class SlicedObjectCounter : MonoBehaviour
{

    FruitsChecker fruitchecker;

    private void Awake()
    {
        GameSequencer.ItemMoveStartListener += MoveObjectToMixer;
        GameSequencer.ItemMixStartListener += OnMixStarted;
        fruitchecker = GetComponent<FruitsChecker>();
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

    

    IEnumerator startmixing()
    {
        foreach (var fruit in slicedfruits)
        {
            yield return null;
            fruit.gameObject.SetActive(false);           
          
        }

        yield return new WaitForSeconds(3f);
        destroyAfterSomeTime();

    }

    void destroyAfterSomeTime()
    {
        foreach (var fruit in slicedfruits)
        {

            Destroy(fruit.gameObject);


        }
       
        fruitchecker.allChildobj.Clear();
    }


    public int CountSlicedObjects()
    {
        return fruitchecker.allChildobj.Count;
    }


    void MoveToMixer()
    {
        SetKinematicToAllCutFruits(false);


    }

    public void SetKinematicToAllCutFruits(bool val)
    {
        slicedfruits = fruitchecker.allChildobj.ToArray();
        foreach (var item in slicedfruits)
        {
            item.isKinematic = val;
        }
    }

  

    



}
