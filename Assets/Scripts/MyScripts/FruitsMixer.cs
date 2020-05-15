using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsMixer : MonoBehaviour
{


    public GameObject ObjectToFill;
    [SerializeField] private float maxFillval, minFillVal;

  
    

    public float mixRatio;
    private string  mainColor = "_MainColor";
    public float mixSpeed;

    float fillamount = 0;
    public ClipPlane planeObject;

    Color mixedColor;

    LineRenderer lineRenderer;


    public void SetMixedColor(Color col)
    {
        mixedColor = col;
        Material mat = ObjectToFill.GetComponent<Renderer>().material;
        mat.SetColor(mainColor, mixedColor);
    }

    private void Awake()
    {
        GameSequencer.ItemMixStartListener += OnMixingStart;
        lineRenderer = GetComponent<LineRenderer>();
       
    }

    void OnMixingStart()
    {
        fillamount = minFillVal;
        planeObject.transform.SetPosition(y: minFillVal);
        StartMixingFruits(true);
    }
   


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            fillamount = minFillVal;
            planeObject.transform.SetPosition(y: minFillVal);
            StartMixingFruits(true);
        }
    }
    public void StartMixingFruits(bool fillFlask)
    {

        mixedColor= GameSequencer.Instance.mixedColor;
        lineRenderer.material = ObjectToFill.GetComponent<Renderer>().sharedMaterial;

        Material mat = ObjectToFill.GetComponent<Renderer>().material;
        mat.SetColor(mainColor, mixedColor);
        if (fillFlask)
        {
        StartCoroutine(FillFlask(mat,minFillVal,maxFillval,fillFlask));

        }else
        StartCoroutine(FillFlask(mat,minFillVal,maxFillval,fillFlask));

        // run particle system for mixing objects



    }

    IEnumerator FillFlask(Material mat , float minfill, float maxfill ,bool fillflask)
    {
        
        planeObject.transform.SetPosition(y: minfill);
        fillamount = fillflask ? minfill : maxfill;
        Vector3 localpos = planeObject.transform.localPosition;
       

        if (fillflask)
        {
            while (fillamount < maxfill)
            {
                fillamount += mixSpeed * Time.deltaTime;
                localpos.y = fillamount;
                planeObject.transform.localPosition = localpos;

                yield return null;
            }
        }
        else
        {
            while (fillamount > minfill)
            {
                fillamount -= mixSpeed * Time.deltaTime;
                localpos.y = fillamount;
                planeObject.transform.localPosition = localpos;


                yield return null;
            }
        }

      

    }


   
}
