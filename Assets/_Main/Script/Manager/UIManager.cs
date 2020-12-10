using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region SingleTon
    /* SingleTon */
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;
                if (!instance)
                {
                    Debug.LogError("UIManager Not Exsist In This Scene");
                }
            }

            return instance;
        }
    }

    #endregion


    #region Values

    public Image LifeBar;

    public TextMeshProUGUI JudgementText;

    public Color32 color_Gray_Off;
    public Color32 color_Gray_On;

    public Color32 color_Blue_Off;
    public Color32 color_Blue_On;

    public Image[] Line;

    public Note NotePrefab;

    #endregion

    private void Awake()
    {
        #region SingleTone

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        #endregion
    }



    public void Init()
    {
        UIInitLifeBar();
        JudgementInit();
    }

    #region Judgement

    public void JudgementInit()
    {
        JudgementText.gameObject.SetActive(false);
    }

    public void SetJudgement(EnumJudgement judge)
    {
        // DoTween 제거, 초기화
        JudgementText.DOKill(false);
        JudgementText.DOFade(1.0f, 0);
        JudgementText.rectTransform.localScale = Vector3.one;

        
        switch (judge)
        {
            case EnumJudgement.Bad:
                JudgementText.text = "나쁨!";
                break;
            case EnumJudgement.Good:
                JudgementText.text = "좋음!";
                break;
        }

        JudgementText.gameObject.SetActive(true);


        JudgementText.rectTransform.DOScale(1.5f, 0.5f);
        JudgementText.DOFade(0f, 1.0f).OnComplete(() =>
        {
            JudgementText.gameObject.SetActive(false);
        });
    }

    #endregion


    #region Life Bar

    public void UIInitLifeBar()
    {
        LifeBar.fillAmount = 1.0f;
    }

    public void UIUpdateLifeBar(float rate)
    {
        LifeBar.DOComplete(false);
        LifeBar.DOFillAmount(rate, 0.2f);
    }

    #endregion


    #region Line Effect

    public void LineEffect(int lineNumber)
    {
        if(lineNumber == 0 || lineNumber == 3)
        {
            Line[lineNumber].DOKill(false);
            Line[lineNumber].DOColor(color_Gray_On, 0.1f).OnComplete(() => {
                Line[lineNumber].DOColor(color_Gray_Off, 0.1f);
            });
        }
        else
        {
            Line[lineNumber].DOKill(false);
            Line[lineNumber].DOColor(color_Blue_On, 0.1f).OnComplete(() => {
                Line[lineNumber].DOColor(color_Blue_Off, 0.1f);
            });
        }

        
    }

    #endregion

    public void SetNote(int index, int lineNumber)
    {
        Note n = Instantiate(NotePrefab);
        n.gameObject.name = string.Format("Note[{0}]", index);

        n.rt.SetParent(Line[lineNumber].transform, false);
        n.rt.anchoredPosition = new Vector2(0, 400);

        n.StartNote(lineNumber);
    }

}
