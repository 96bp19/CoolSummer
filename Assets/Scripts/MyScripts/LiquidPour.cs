using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidPour : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform pourStartTransform;

    [SerializeField] Animator animController;

    // related to linerender
    [SerializeField] float liquidDropSpeed =1;
    [SerializeField] float liquidReturnSpeed = 1;


    [SerializeField] GameObject FilledFlask, EmptyFlask;
    // related to filling or emptying speed of bottle
    [SerializeField] float liquidPourSpeed;

    bool liquidReachedtheButtom;

    private void Start()
    {
        lineRenderer.SetPosition(0, pourStartTransform.position);
        lineRenderer.SetPosition(1, pourStartTransform.position);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pour();
        }
    }


    void Pour()
    {
        Material filledMat = FilledFlask.GetComponent<Renderer>().material;
        Material emptymat = EmptyFlask.GetComponent<Renderer>().material;
        StartCoroutine(PourLiquid(filledMat, emptymat, liquidPourSpeed));
    }

    IEnumerator PourLiquid( Material filledflaskmat, Material emptyflaskmat ,float pourSpeed)
    {

        float currentLiquidAmount = filledflaskmat.GetFloat("Vector1_C7F75E1D");
        
        IEnumerator endposRoutine = null;
        endposRoutine = SetEndPosForLine(liquidDropSpeed, pourStartTransform.position + Vector3.down * 3,1);
        StartCoroutine(endposRoutine);
        while(!liquidReachedtheButtom)
        {
            // just return until liquid hits buttom
            yield return null;
        }
        while(currentLiquidAmount >=-1)
        {
            emptyflaskmat.SetFloat("Vector1_C7F75E1D", emptyflaskmat.GetFloat("Vector1_C7F75E1D")+pourSpeed*Time.deltaTime);
            currentLiquidAmount -= pourSpeed * Time.deltaTime;
            filledflaskmat.SetFloat("Vector1_C7F75E1D", currentLiquidAmount);
            yield return null;
        }
        Debug.Log("pour complete");
        StopCoroutine(endposRoutine);
        endposRoutine = SetEndPosForLine(liquidDropSpeed/2, pourStartTransform.position + Vector3.down * 3, 0);
        StartCoroutine(endposRoutine);

    }

    IEnumerator SetEndPosForLine(float speed, Vector3 newPos , int index)
    {
        Vector3 currentPos = lineRenderer.GetPosition(index);
        Vector3 startpos = currentPos; 
        float currentTime = 0;
        while (currentTime <=1)
        {
            currentTime += speed * Time.deltaTime;
            currentPos = Vector3.Lerp(startpos, newPos, currentTime);
            MoveLineRendererEndPos(index,currentPos);
            yield return null;
        }
        if (index ==1)
        {
            liquidReachedtheButtom = true;
        }
    }

    void MoveLineRendererEndPos(int index ,Vector3 newpos)
    {
        lineRenderer.SetPosition(index, newpos);
    }
}
