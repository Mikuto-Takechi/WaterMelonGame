using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] FruitList _fruitList;
    [SerializeField] Transform _viewPort;
    [SerializeField] float _stopIntervalLine = 6;
    [SerializeField] float _moveSpeed = 1.0f;
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
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;
        transform.position += moveForward * _moveSpeed;
        Vector3 center = new Vector3(0, 12.67f, 0);
        Vector3 offset = transform.position - center;
        transform.position = center + Vector3.ClampMagnitude(offset, 4);
        if (Input.GetButton("Fire1") && _createFruitFlag)
        {
            _createFruitFlag = false;
            AudioManager.instance.PlaySE("Œˆ’èƒ{ƒ^ƒ“‚ð‰Ÿ‚·44");
            DropFruit();
        }
    }
    void CreateFruit()
    {
        int num = Random.Range(0, 4);
        switch (num)
        {
            case 0:
                _nextFruit = Instantiate(_fruitList.Level0, _viewPort);
                break;
            case 1:
                _nextFruit = Instantiate(_fruitList.Level1, _viewPort);
                break;
            case 2:
                _nextFruit = Instantiate(_fruitList.Level2, _viewPort);
                break;
            case 3:
                _nextFruit = Instantiate(_fruitList.Level3, _viewPort);
                break;
            case 4:
                _nextFruit = Instantiate(_fruitList.Level4, _viewPort);
                break;
        }
        _nextFruit.layer = 6;
        _nextFruit.GetComponent<Rigidbody>().isKinematic = true;
    }
    void SetFruit()
    {
        _dropFruit = _nextFruit;
        _dropFruit.layer = 0;
        _dropFruit.transform.SetParent(transform);
        Vector3 dropPosition = transform.position;
        dropPosition.y += -1;
        _dropFruit.transform.position = dropPosition;
    }
    void DropFruit()
    {
        _dropFruit.transform.SetParent(null);
        _dropFruit.GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(Interval(_dropFruit.transform));
    }
    IEnumerator Interval(Transform trans)
    {
        while (true)
        {
            if(trans == null) yield break;
            if(trans.position.y <= _stopIntervalLine)
            {
                _createFruitFlag = true;
                SetFruit();
                CreateFruit();
                yield break;
            }
            yield return null;
        }
    }
}
