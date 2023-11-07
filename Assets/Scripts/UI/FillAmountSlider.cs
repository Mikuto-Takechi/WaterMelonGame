using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// Image�R���|�[�l���g��fillAmount���g�����X���C�_�[
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
    /// fillAmount�̒l�ł܂݂̈ʒu�𒲐�����
    /// </summary>
    void UpdateHandlePosition()
    {
        // �܂݂�RectTransform�̔����̒��������߂�
        var halfHandleSize = _handleRect.sizeDelta / 2;
        // Lerp���\�b�h���g��fillAmount�̊����ł܂݂̈ʒu�����߂�
        var handlePos = Vector2.Lerp(_rect.offsetMin, _rect.offsetMax, _fillImage.fillAmount);
        // �i���[ + �܂݂̔��� �` �E�[ - �܂݂̔����j�͈̔͂Œl�𐧌�����
        handlePos.x = Mathf.Clamp(handlePos.x, (_rect.offsetMin + halfHandleSize).x, (_rect.offsetMax - halfHandleSize).x);
        handlePos.y = _handleRect.anchoredPosition.y;
        _handleRect.anchoredPosition = handlePos;
    }
    /// <summary>
    /// fillAmount�̒l���J�[�\���̈ʒu�ɍ��킹��
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