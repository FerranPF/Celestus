using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PauseTweens : MonoBehaviour
{
    public RectTransform[] pauseCont;
    private float cont;

    void OnEnable()
    {
        for(int i = 0; i < pauseCont.Length; i++)
        {
            pauseCont[i].DOAnchorPosY(2000, 0.5f).From();
        }
    }

    private void Update()
    {
        cont += Time.deltaTime;
        if(cont >= 0.5f)
        {
            Time.timeScale = 0;
        }
    }

    private void OnDisable()
    {
        cont = 0.0f;
        Time.timeScale = 1;
    }
}
