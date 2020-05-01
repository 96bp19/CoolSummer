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
        GetComponent<Collider>().material = physicsMAt;
        MoveToMixer(slicedfruits);
        Invoke("OnMoveComplete", 2.5f);
    }

    
    void OnMixStarted()
    {
        StartCoroutine(startmixing());

      
    }

     void dosakjsi(GameObject a)
    {
        Destroy(a);
    }

    IEnumerator startmixing()
    {
            Debug.Log("item count : " + slicedfruits.Length);
        int count = 0;

        var item = slicedfruits;
//         while (count < slicedfruits.Length-1)
//         {
//             Debug.Log("destroyed");
//             count++;
//             yield return null;
//             dosakjsi(item[count].gameObject);
// 
//         }

        foreach (var fruit in slicedfruits)
        {
            yield return null;
            Destroy(fruit.gameObject);
        }

            Debug.Log("loop finish after count  : " +count);
    
    }


    public int CountSlicedObjects()
   {
        return transform.childCount;
   }

    public PhysicMaterial physicsMAt;
  

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
           
            item.transform.position = new Vector3(0, Random.Range(3f, 6f),0);
        }


    }

  

    



}
