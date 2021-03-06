﻿using System.Collections;
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


    [SerializeField] FruitsMixer FilledFlask;
    bool liquidReachedtheButtom;


    private string  mainColor = "Color_EE88DBB1", SecondaryColor = "Color_2410312E";

    private void Awake()
    {
        GameSequencer.ItemPourStartListener += OnPourStart;
    }

    private void Start()
    {
        resetLineRendererPos();
        lineRenderer.enabled = false;
    }

    void OnPourStart()
    {
        resetLineRendererPos();
        Pour();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            resetLineRendererPos();
            Pour();
        }
    }
    
    void resetLineRendererPos()
    {
        lineRenderer.SetPosition(0, pourStartTransform.position);
        lineRenderer.SetPosition(1, pourStartTransform.position);
    }

    void Pour()
    {
     //   EmptyFlask.mixColors = FilledFlask.mixColors;
     //   EmptyFlask.SetMixedColor(GameSequencer.Instance.mixedColor);
        StartCoroutine(PourLiquid(FilledFlask));
    }

    IEnumerator PourLiquid( FruitsMixer filledflask)
    {

        lineRenderer.enabled = true;
        IEnumerator endposRoutine = null;
        liquidReachedtheButtom = false;
        endposRoutine = SetEndPosForLine(liquidDropSpeed, pourStartTransform.position + Vector3.down * 3,1);
        StartCoroutine(endposRoutine);
        while(!liquidReachedtheButtom)
        {
            // just return until liquid hits buttom
            yield return null;
        }
        filledflask.StartMixingFruits(false);
      //  emptyflask.StartMixingFruits(true);
     
        StopCoroutine(endposRoutine);
        endposRoutine = SetEndPosForLine(liquidDropSpeed/2, pourStartTransform.position + Vector3.down * 3, 0);
        Debug.Log("second routine");
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
        else
        {
            // pour complete
            GameSequencer.Instance.OnItemPoured();
            lineRenderer.enabled = false;
        }
    }

    void MoveLineRendererEndPos(int index ,Vector3 newpos)
    {
        lineRenderer.SetPosition(index, newpos);
    }

    
}
