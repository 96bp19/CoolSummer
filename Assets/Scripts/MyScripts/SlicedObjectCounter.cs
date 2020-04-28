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
        GetComponent<MeshCollider>().material = physicsMAt;
        MoveToMixer(slicedfruits);
        Invoke("OnMoveComplete", 2.5f);
    }

    
    void OnMixStarted()
    {
        StartCoroutine(startmixing());
    }

     void dosakjsi(GameObject a)
    {
        a.SetActive(false);
    }

    IEnumerator startmixing()
    {
            Debug.Log("item count : " + slicedfruits.Length);
        int count = 0;

        var item = slicedfruits;
        while (count < slicedfruits.Length)
        {
            dosakjsi(item[count].gameObject);
            Debug.Log("destroyed");
            count++;
            yield return null;

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

        // Time.timeScale = 0.1f;
       

        foreach (var item in objects)
        { 
            item.gameObject.layer = LayerMask.NameToLayer("ChoppedFruits");
            item.transform.SetParent(null);
           
            item.transform.position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(3f, 6f), Random.Range(-0.5f, 0.5f));
        }


    }

  

    



}
