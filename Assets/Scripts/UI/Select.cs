using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Select : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float _animationTime = 0.5f;
    Color _baseColor;
    Image _image;
    Color _mouseOverColor = new Color(0.5f, 0.5f, 0.5f);
    private void Start()
    {
        _image = GetComponent<Image>();
        _baseColor = _image.color;
    }
    public void OnSelect(BaseEventData eventData)
    {
        transform.DOScale(1.2f, _animationTime).SetLink(gameObject);
        _image.DOColor(_mouseOverColor, _animationTime).SetLink(gameObject);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        transform.DOScale(1, _animationTime).SetLink(gameObject);
        _image.DOColor(_baseColor, _animationTime).SetLink(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.2f, _animationTime).SetLink(gameObject);
        _image.DOColor(_mouseOverColor, _animationTime).SetLink(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, _animationTime).SetLink(gameObject);
        _image.DOColor(_baseColor, _animationTime).SetLink(gameObject);
    }
}
