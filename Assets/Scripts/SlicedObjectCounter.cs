using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedObjectCounter : MonoBehaviour
{
    public Animator anim;

    private void Awake()
    {
        GameSequencer.ItemMoveStartListener += MoveObjectToMixer;
    }

    void MoveObjectToMixer()
    {
        var objects = transform.GetComponentsInChildren<Rigidbody>();
        GetComponent<MeshCollider>().material = physicsMAt;
        MoveToMixer(objects);
        Invoke("OnMoveComplete", 2.5f);
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

        anim.SetTrigger("MoveToMixer");


    }

  

    



}
