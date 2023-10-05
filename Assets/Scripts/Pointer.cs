using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] FruitList _fruitList;
    [SerializeField] Transform _viewPort;
    [SerializeField] float _interval = 2;
    [SerializeField] float _moveSpeed = 1.0f;
    [SerializeField] Animator _handAnimator;
    GameObject _nextFruit;
    GameObject _dropFruit;
    bool _createFruitFlag = true;
    float inputHorizontal;
    float inputVertical;
    void Start()
    {
        CreateFruit();
        SetFruit();
        CreateFruit();
    }
    void Update()
    {
        if (GameManager.Instance.GameState == GameState.GameOver) return;
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;
        transform.position += moveForward * _moveSpeed;
        Vector3 center = new Vector3(0, 12.67f, 0);
        Vector3 offset = transform.position - center;
        transform.position = center + Vector3.ClampMagnitude(offset, 4);
        _handAnimator.transform.forward = cameraForward;
        if (Input.GetButton("Fire1") && _createFruitFlag)
        {
            _createFruitFlag = false;
            _handAnimator.Play("Idle");
            AudioManager.instance.PlaySE("Œˆ’èƒ{ƒ^ƒ“‚ð‰Ÿ‚·44");
            DropFruit();
        }
    }
    void CreateFruit()
    {
        _nextFruit = Random.Range(0, 4) switch
        {
            0 => Instantiate(_fruitList.Level0, _viewPort),
            1 => Instantiate(_fruitList.Level1, _viewPort),
            2 => Instantiate(_fruitList.Level2, _viewPort),
            3 => Instantiate(_fruitList.Level3, _viewPort),
            4 => Instantiate(_fruitList.Level4, _viewPort),
            _ => throw new System.NotImplementedException(),
        };
        _nextFruit.layer = 6;
        _nextFruit.GetComponent<Rigidbody>().isKinematic = true;
    }
    void SetFruit()
    {
        if (_nextFruit.gameObject == null) return;
        _dropFruit = _nextFruit;
        _dropFruit.layer = 0;
        _dropFruit.transform.SetParent(transform);
        Vector3 dropPosition = transform.position;
        dropPosition.y += -2;
        _dropFruit.transform.position = dropPosition;
    }
    void DropFruit()
    {
        if (_dropFruit.gameObject == null) return;
        _dropFruit.transform.SetParent(null);
        _dropFruit.GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(Interval());
    }
    IEnumerator Interval()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if(timer > _interval)
            {
                _createFruitFlag = true;
                _handAnimator.Play("Grab");
                SetFruit();
                CreateFruit();
                yield break;
            }
            yield return null;
        }
    }
}
