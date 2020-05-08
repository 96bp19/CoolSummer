using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeRandomRotater : MonoBehaviour
{
    bool useKnifeRandomRotation;
    bool animatorval;
    public Animator anim;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            useKnifeRandomRotation = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            useKnifeRandomRotation = false;
        }
        UpdateAnimatorState();
    }

    void UpdateAnimatorState()
    {
        if (animatorval == false && useKnifeRandomRotation)
        {
            animatorval = true;
            anim.SetTrigger("CutRandom");
        }
        else if(animatorval && !useKnifeRandomRotation)
        {
            animatorval = false;
            anim.SetTrigger("Idle");
        }
    }
}
