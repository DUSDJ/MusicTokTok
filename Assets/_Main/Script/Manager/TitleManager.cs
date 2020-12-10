using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public RectTransform TitleImage;
    
    private void Start()
    {
        TitleImage.DOScale(1.0f, 0.7f).OnComplete(() =>
        {
            TitleImage.DOShakeScale(1.0f, 0.2f, 5).SetLoops(-1);
        });
    }


    public void BtnStart()
    {
        SceneManager.LoadScene(1);
    }
}
