using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAnimationPlayer : MonoBehaviour
{

    public GameObject CutParticle;
    public Transform knifeCutLocation;

    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Cut");
            Instantiate(CutParticle, knifeCutLocation.position, Quaternion.identity);
            
        }
    }
}
