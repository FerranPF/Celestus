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
    // Start is called before the first frame update
    void Start()
    {
        newgame.DOAnchorPosX(-2111, 1f).From();
        options.DOAnchorPosX(-2111, 1f).From();
        exit.DOAnchorPosX(-2111, 1f).From();
        
    }


 public void OptionButtonPressed()
    {
        newgame.DOAnchorPosX(-2111, 1f);
        options.DOAnchorPosX(-2111, 1f);
        exit.DOAnchorPosX(-2111, 1f);
        optionpanel.DOAnchorPosX(-520, 1f).SetDelay(1f);

    }
    // Update is called once per frame
   public void ButtonBack()
    {
        newgame.DOAnchorPosX(1, 1f);
        options.DOAnchorPosX(1, 1f);
        exit.DOAnchorPosX(1, 1f);
        newgame.DOAnchorPosX(-2111, 1f);
        options.DOAnchorPosX(-137, 1f);
        exit.DOAnchorPosX(137, 1f);
        optionpanel.DOAnchorPosX(137, 1f).SetDelay(1f);
    }
}
