using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeRotater : MonoBehaviour
{

    public GameObject choppingboard;
    public GameObject knifeRoot;

    private void Update()
    {
        Vector3 newRot = Quaternion.LookRotation((choppingboard.transform.position - knifeRoot.transform.position).normalized, Vector3.up).eulerAngles;
        Vector3 knifeRot = knifeRoot.transform.eulerAngles;
        knifeRot.y = newRot.y;
        knifeRot.x = knifeRot.z = 0f;
        knifeRoot.transform.eulerAngles = knifeRot;

    

    }
}
