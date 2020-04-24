using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractObjects : MonoBehaviour
{
    public float affectRadius = 3;
    public float pullForce = 5;

    List<Rigidbody> affectedRigidbodies = new List<Rigidbody>();
    Vector3 moveDirection = Vector3.zero;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var affectedbodies = Physics.OverlapSphere(transform.position, affectRadius);
            affectedRigidbodies = new List<Rigidbody>();
            foreach (var item in affectedbodies)
            {
                if (item.GetComponent<Rigidbody>())
                {
                    affectedRigidbodies.Add(item.GetComponent<Rigidbody>());

                }
            }

        }

            foreach (var item in affectedRigidbodies)
            {
                moveDirection = transform.position - item.position;
                item.AddForce(moveDirection * pullForce, ForceMode.Force);
            }

    }
}
