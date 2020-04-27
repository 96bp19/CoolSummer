using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedObjectCounter : MonoBehaviour
{
    public Animator anim;
  
    public int CountSlicedObjects()
   {
        return transform.childCount;
   }

    public PhysicMaterial physicsMAt;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            var objects = transform.GetComponentsInChildren<Rigidbody>();
            GetComponent<MeshCollider>().material = physicsMAt;
            MoveToMixer(objects);

        }
    }

    


    void MoveToMixer(Rigidbody[] objects)
    {
        anim.SetTrigger("MoveToMixer");


    }

    



}
