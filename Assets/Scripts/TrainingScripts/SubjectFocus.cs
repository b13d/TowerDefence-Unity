using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubjectFocus : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _squares = new List<GameObject>();

    [SerializeField]
    GameObject _cursor;

    [SerializeField]
    TextMeshProUGUI _txtTap;


    void Start()
    {
        //TextTap();
        CursorClick();
        //SquareToX();
        //RotateAloneSquare();
    }

    void TextTap()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.SetLoops(-1);

        sequence.Append(_txtTap.transform.DOScale(2, .5f));
        sequence.Append(_txtTap.transform.DOScale(1, .2f));
        sequence.Append(_txtTap.DOColor(Color.gray, .1f));
    }

    void CursorClick()
    {
        _cursor.transform.DORotate(new Vector3(0, 0, 15), .2f).SetLoops(-1, LoopType.Yoyo);
    }

    void SquareToX()
    {
        for(int i = 0; i < _squares.Count; i++)
        {
            _squares[i].transform.DOMoveX(0, 1f).SetLoops(-1, LoopType.Yoyo);
        }
    }

    void RotateAloneSquare()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.SetLoops(-1);

        sequence.Append(transform.DORotate(new Vector3(0, 0, 180f), 1f));
        sequence.Append(transform.DOScale(new Vector3(1f, 1f, 1f), 1f));
        sequence.Append(transform.DORotate(new Vector3(0, 0, 360f), 1f));
        sequence.Append(transform.DOScale(new Vector3(2f, 2f, 2f), 1f));
    }
}
