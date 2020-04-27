using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FruitsChecker : MonoBehaviour
{
    public SlicedObjectCounter counter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fruits"))
        {
            other.gameObject.tag = "Sliceable";
            other.transform.SetParent(transform);

        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sliceable"))
        {
           // StartCoroutine(destroyAfterSec(other.gameObject));

        }
    }

    IEnumerator destroyAfterSec(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        if (obj)
        {
            Destroy(obj);
        }
    }

}
