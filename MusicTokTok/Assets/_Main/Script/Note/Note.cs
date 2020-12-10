using DG.Tweening;
using System;
using System.Collections.Generic;

using UnityEngine;

public class Note : MonoBehaviour
{
    public RectTransform rt;
    public int LineNumber;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    public void StartNote(int lineNumber)
    {
        LineNumber = lineNumber;

        GameManager.Instance.ListNoteList[LineNumber].Add(this);

        rt.DOKill();
        rt.DOAnchorPosY(-200, GameManager.Instance.NoteArriveTime).SetEase(Ease.Linear).OnComplete(()=> {
            
            rt.DOAnchorPosY(-225, (float)25/600 * GameManager.Instance.NoteArriveTime).SetEase(Ease.Linear).OnComplete(() => {
                FailNote();
            });                
        });
    }

    public void CheckNote()
    {
        if (rt.anchoredPosition.y >= -200 + 25)
        {
            // 근접했는데 미스뜬거
            if (rt.anchoredPosition.y <= -150)
            {
                // Fail=
                FailNote();
            }
            // 이 정도 선입력은 봐줌
            else
            {
                return;
            }
        }

        if (rt.anchoredPosition.y <= -175 && rt.anchoredPosition.y > -225)
        {
            // Clear
            ClearNote();
        }

    }

    public void ClearNote()
    {
        Debug.Log(gameObject.name + "ClearNote");

        rt.DOKill();

        GameManager.Instance.ListNoteList[LineNumber].Remove(this);

        UIManager.Instance.SetJudgement(EnumJudgement.Good);
        gameObject.SetActive(false);
    }

    public void FailNote()
    {
        Debug.Log(gameObject.name + " FailNote");

        rt.DOKill();

        GameManager.Instance.ListNoteList[LineNumber].Remove(this);
        GameManager.Instance.HP -= 1;

        UIManager.Instance.SetJudgement(EnumJudgement.Bad);
        gameObject.SetActive(false);
    }

}
