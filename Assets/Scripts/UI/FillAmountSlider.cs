using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// ImageコンポーネントのfillAmountを使ったスライダー
/// </summary>
[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class FillAmountSlider : Selectable, IDragHandler, IPointerDownHandler
{
    [SerializeField] RectTransform _handleRect;
    [SerializeField] Image _fillImage;
    [SerializeField] Image _fillFadeImage;
    [SerializeField, Range(0, 1)] float _fillAmount = 1;

    RectTransform _rect;
    Vector2 _offset = Vector2.zero;
    protected override void Start()
    {
        base.Start();
        _rect = GetComponent<RectTransform>();
        Initialize();
    }
#if UNITY_EDITOR
    protected override void OnValidate() 
    {
        base.OnValidate();
        _rect = GetComponent<RectTransform>();
        Initialize();
    } 
#endif
    void Initialize()
    {
        if (_fillImage && _fillFadeImage)
        {
            _fillImage.fillAmount = _fillAmount;
            _fillFadeImage.fillAmount = _fillAmount;
            UpdateHandlePosition();
        }
    }
    void Set(float changeAmount)
    {
        _fillImage.fillAmount = changeAmount;
        DOVirtual.Float(_fillFadeImage.fillAmount, changeAmount, 0.5f, x => _fillFadeImage.fillAmount = x);
        UpdateHandlePosition();
    }
    public override void OnMove(AxisEventData eventData)
    {
        if (!IsActive() || !IsInteractable())
        {
            base.OnMove(eventData);
            return;
        }

        switch (eventData.moveDir)
        {
            case MoveDirection.Left:
                if (FindSelectableOnLeft() == null)
                    Set(_fillImage.fillAmount - 0.1f);
                else
                    base.OnMove(eventData);
                break;
            case MoveDirection.Right:
                if (FindSelectableOnRight() == null)
                    Set(_fillImage.fillAmount + 0.1f);
                else
                    base.OnMove(eventData);
                break;
        }
    }
    /// <summary>
    /// fillAmountの値でつまみの位置を調整する
    /// </summary>
    void UpdateHandlePosition()
    {
        // つまみのRectTransformの半分の長さを求める
        var halfHandleSize = _handleRect.sizeDelta / 2;
        // Lerpメソッドを使いfillAmountの割合でつまみの位置を決める
        var handlePos = Vector2.Lerp(_rect.offsetMin, _rect.offsetMax, _fillImage.fillAmount);
        // （左端 + つまみの半分 ～ 右端 - つまみの半分）の範囲で値を制限する
        handlePos.x = Mathf.Clamp(handlePos.x, (_rect.offsetMin + halfHandleSize).x, (_rect.offsetMax - halfHandleSize).x);
        handlePos.y = _handleRect.anchoredPosition.y;
        _handleRect.anchoredPosition = handlePos;
    }
    /// <summary>
    /// fillAmountの値をカーソルの位置に合わせる
    /// </summary>
    void UpdateDrag(PointerEventData eventData, Camera cam)
    {
        if (_rect.rect.size.x > 0)
        {
            Vector2 localCursor;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_rect, eventData.position, cam, out localCursor))
                return;
            localCursor -= _rect.rect.position;
            var changeAmount = Mathf.Clamp01((localCursor - _offset).x / _rect.rect.size.x);
            Set(changeAmount);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        UpdateDrag(eventData, eventData.pressEventCamera);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        UpdateDrag(eventData, eventData.pressEventCamera);
    }
}