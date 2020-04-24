using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedObjectCounter : MonoBehaviour
{

    public GameObject mixer;
    public int CountSlicedObjects()
   {
        return transform.childCount;
   }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            var objects = transform.GetComponentsInChildren<Rigidbody>();
            MoveToMixer(objects);

        }
    }

    void MoveToMixer(Rigidbody[] objects)
    {
        foreach (var item in objects)
        {
            item.gameObject.layer = LayerMask.NameToLayer("ChoppedFruits");
            item.transform.parent = mixer.transform;
            item.transform.position = new Vector3(0, 2, 0); 
        }
    }
}
