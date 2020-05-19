using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAnimationPlayer : MonoBehaviour
{

    public GameObject CutParticle;
    public Transform knifeCutLocation;

    public Animator anim;

    public delegate void OnCutStart();
    public static OnCutStart CutListener;

    public float cutDelay = 0.3f;

    bool currentlyCutting = false;
    float currentTime = 0;

  

    private void OnDisable()
    {
        
        CancelInvoke("OnCut");
        currentTime = 0;
        currentlyCutting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime >0)
        {
            currentTime -= Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0) && currentlyCutting == false && currentTime <=0f)
        {
            currentlyCutting = true;
            InvokeRepeating("OnCut", 0f, cutDelay);
            
        }
        if (Input.GetMouseButtonUp(0) && currentlyCutting == true)
        {
            CancelInvoke("OnCut");
            currentlyCutting = false;
            currentTime = cutDelay;
        }
    }

    void OnCut()
    {
            anim.SetTrigger("Cut");
            Instantiate(CutParticle, knifeCutLocation.position, Quaternion.identity);
            CutListener?.Invoke();

    }
}
