using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MySlider : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField] RectTransform _handleRect;
    RectTransform _rect;
    Image _image;
    Vector2 _offset = Vector2.zero;
    void Start()
    {
        _rect = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        UpdDrag(eventData, eventData.pressEventCamera);
    }
    void UpdDrag(PointerEventData eventData, Camera cam)
    {
        if (_handleRect.rect.size[0] > 0)// êÖïΩï˚å¸Ç»ÇÃÇ≈[0]  .xÇïtÇØÇÈÇÃÇ∆ìØÇ∂à”ñ°
        {
            Vector2 localCursor;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_handleRect, eventData.position, cam, out localCursor))
                return;
            localCursor -= _handleRect.rect.position;
            _image.fillAmount = Mathf.Clamp01((localCursor - _offset)[0] / _handleRect.rect.size[0]);
            _handleRect.position = new Vector2(eventData.position.x, _handleRect.position.y);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _offset = Vector2.zero;
        if (_handleRect != null && RectTransformUtility.RectangleContainsScreenPoint(_handleRect, eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera))
        {
            Vector2 localMousePos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_handleRect, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out localMousePos))
                _offset = localMousePos;
        }
        else
        {
            // Outside the slider handle - jump to this point instead
            UpdDrag(eventData, eventData.pressEventCamera);
        }
    }
}