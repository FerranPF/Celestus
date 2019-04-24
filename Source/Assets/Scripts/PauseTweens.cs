using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PauseTweens : MonoBehaviour
{
    public RectTransform[] pauseCont;
    private float cont;
    private bool paused;

    void OnEnable()
    {
        paused = false;
        for(int i = 0; i < pauseCont.Length; i++)
        {
            pauseCont[i].DOAnchorPosY(2000, 0.5f).From();
        }
    }

    private void Update()
    {
        if (!paused)
        {
            cont += Time.deltaTime;
            if (cont >= 0.5f)
            {
                paused = true;
                Time.timeScale = 0;
            }
        }
    }



    private void OnDisable()
    {
        cont = 0.0f;
        Time.timeScale = 1;
    }
}
