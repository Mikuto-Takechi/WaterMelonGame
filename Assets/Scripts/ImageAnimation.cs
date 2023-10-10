using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour
{
    Image _image;
    void Start()
    {
        _image = GetComponent<Image>();
        _image.rectTransform.DOLocalMoveY(GetComponent<RectTransform>().anchoredPosition.y + 30, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);
    }

    void Update()
    {
        
    }
}
