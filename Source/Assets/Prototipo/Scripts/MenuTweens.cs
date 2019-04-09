using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MenuTweens : MonoBehaviour
{
    public RectTransform newgame;
    public RectTransform options;
    public RectTransform exit;
    public RectTransform optionpanel;
    public GameObject par1;
    public GameObject par2;
    public GameObject Floor1;
    public GameObject Floor2;

    private float buttonsPos;
    
    // Start is called before the first frame update
    void Start()
    {
        newgame.DOAnchorPosX(-2111, 1f).From();
        options.DOAnchorPosX(-2111, 1f).From();
        exit.DOAnchorPosX(-2111, 1f).From();
        par2.SetActive(true);
        par1.SetActive(false);
        Floor2.SetActive(false);
        Floor1.SetActive(true);
        buttonsPos = newgame.transform.position.x;
    }


 public void OptionButtonPressed()
    {
        par2.SetActive(false);
        newgame.DOAnchorPosX(-2000, 1f);
        options.DOAnchorPosX(-2000, 1f);
        exit.DOAnchorPosX(-2500, 1f);
        optionpanel.DOAnchorPosX(-520, 1f).SetDelay(1f);
        par1.SetActive(true);
        Floor2.SetActive(true);
        Floor1.SetActive(false);
        

    }
    // Update is called once per frame
   public void ButtonBack()
    {
        par2.SetActive(true);
        newgame.DOAnchorPosX(0, 1f).SetDelay(1f);
        options.DOAnchorPosX(0, 1f).SetDelay(1f);
        exit.DOAnchorPosX(0, 1f).SetDelay(1f);
        optionpanel.DOAnchorPosX(-2111, 1f);
        par1.SetActive(false);
        Floor2.SetActive(false);
        Floor1.SetActive(true);
    }
}
