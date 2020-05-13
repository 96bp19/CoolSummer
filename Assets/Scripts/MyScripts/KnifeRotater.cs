using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeRotater : MonoBehaviour
{

    public GameObject choppingboard;
    public GameObject knifeRoot;
    public Transform randomRotator;
    public float rotationchangeDelay = 3f;
    public float rotationchangeSmoothness;
    private void Update()
    {
        //         Vector3 newRot = Quaternion.LookRotation((choppingboard.transform.position - knifeRoot.transform.position).normalized, Vector3.up).eulerAngles;
        //         Vector3 knifeRot = knifeRoot.transform.eulerAngles;
        //         knifeRot.y = newRot.y;
        //         knifeRot.x = knifeRot.z = 0f;
        //         knifeRoot.transform.eulerAngles = knifeRot;

        // knifeRoot.transform.localRotation = randomRotator.rotation;

       // knifeRoot.transform.localEulerAngles = Vector3.Lerp(knifeRoot.transform.localEulerAngles, newRot, rotationchangeSmoothness * Time.deltaTime);

        float y = Mathf.LerpAngle(knifeRoot.transform.localEulerAngles.y, newRot.y, rotationchangeSmoothness * Time.deltaTime);
      
        knifeRoot.transform.localEulerAngles = newRot;

    }

    private void OnDisable()
    {
        CancelInvoke("ChanngeRotation");
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        InvokeRepeating("ChanngeRotation", rotationchangeDelay, rotationchangeDelay);
    }

    Vector3 newRot = Vector3.zero;

    void ChanngeRotation()
    {
        int randomval = Random.Range(-2, 2);
        Vector3 localeuler = knifeRoot.transform.localEulerAngles;

        float newval = randomval * 15f + 15f + localeuler.y;
        newval = ((int)newval % 55);
        localeuler.y = newval;
        localeuler.x = localeuler.z = 0f;
        newRot = localeuler;
       ;
    }
}
