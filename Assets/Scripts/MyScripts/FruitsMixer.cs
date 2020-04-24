using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsMixer : MonoBehaviour
{
    public List<GameObject> usedFruits = new List<GameObject>();

    public GameObject ObjectToFill;
    [SerializeField] private float maxFillval, minFillVal;

    public Color[] mixColors;
    

    public float mixRatio;
    private string  mainColor = "_MainColor";
    public float mixSpeed;

    float fillamount = 0;
    public ClipPlane planeObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            fillamount = minFillVal;
            planeObject.transform.SetPosition(y: minFillVal);
            StartMixingFruits();
        }
    }
    public void StartMixingFruits()
    {

        Color mixedfruitColor = Color.red;
        Material mat = ObjectToFill.GetComponent<Renderer>().material;
        mat.SetColor(mainColor, mixedfruitColor);
        StartCoroutine(FillFlask(mat));
        // run particle system for mixing objects



    }

    IEnumerator FillFlask(Material mat)
    {
        while (planeObject.transform.position.y <maxFillval)
        {
            fillamount += mixSpeed * Time.deltaTime;
            planeObject.transform.SetPosition(y :fillamount);


            yield return null;
        }

    }




    

    private void OnDrawGizmos()
    {
     
//         Gizmos.color = ColorConverter.MixColor(mixColors);
//         Gizmos.DrawCube(Vector3.zero,Vector3.one);
//         
//         Gizmos.color = (mixColors[0] + mixColors[1]) / 2;
//         Gizmos.DrawCube(Vector3.right * 2, Vector3.one);
// 
//         Gizmos.color = ColorConverter.Mix_Color(mixRatio, mixColors[0], mixColors[1]);
//         Gizmos.DrawCube(Vector3.right * 4, Vector3.one);
// 
//         mixColors[2] = ColorConverter.SubtractiveMixColor(mixColors[0], mixColors[1], mixRatio); 
//         Gizmos.color = mixColors[2];
//         Gizmos.DrawCube(Vector3.right * 6, Vector3.one);
// 
//         Gizmos.color = ColorConverter.SubtractiveMixColor(mixColors[0], mixColors[1], mixRatio,true);
//         Gizmos.DrawCube(Vector3.right * 8, Vector3.one);



    }

   
}
