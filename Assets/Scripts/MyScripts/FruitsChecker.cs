using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsChecker : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fruits"))
        {
            other.gameObject.tag = "Sliceable";
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sliceable"))
        {
            other.gameObject.tag = "Fruits";
            Debug.Log("Out of chopping Board");
        }
    }
}
