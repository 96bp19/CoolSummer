using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedObjectCounter : MonoBehaviour
{
    public Animator anim;

    private void Awake()
    {
        GameSequencer.ItemMoveStartListener += MoveObjectToMixer;
        GameSequencer.ItemMixStartListener += OnMixStarted;
    }

    Rigidbody[] slicedfruits;
    void MoveObjectToMixer()
    {
        slicedfruits = transform.GetComponentsInChildren<Rigidbody>();
        MoveToMixer(slicedfruits);
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
            Destroy(fruit.gameObject);
        }

    }


    public int CountSlicedObjects()
    {
        return transform.childCount;
    }


    void OnMoveComplete()
    {
        GameSequencer.Instance.OnItemMovedToBlender();
    }


    void MoveToMixer(Rigidbody[] objects)
    {

        //  anim.SetTrigger("MoveToMixer");

    
       

        foreach (var item in objects)
        { 
            item.gameObject.layer = LayerMask.NameToLayer("ChoppedFruits");
            item.transform.SetParent(null);
           
            item.transform.position = new Vector3(0, Random.Range(2f, 3f),0.5f);
        }


    }

  

    



}
