using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] FruitList _fruitList;
    [SerializeField] Transform _viewPort;
    [SerializeField] float _stopIntervalLine = 6;
    GameObject _nextFruit;
    float _horizontal = 0;
    float _x = 0;
    bool _createFruitFlag = true;
    void Start()
    {
        _x = transform.position.x;
        CreateFruit();
    }
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _x += _horizontal / 60;
        _x = Mathf.Clamp(_x, -6.5f, 6.5f);
        transform.position = new Vector3(_x, transform.position.y, transform.position.z);
        if(_createFruitFlag)
        {
            _createFruitFlag = false;
            CreateFruit();
        }
    }
    void CreateFruit()
    {
        int num = Random.Range(0, 4);
        if(_nextFruit)
        {
            _nextFruit.transform.SetParent(null);
            _nextFruit.transform.position = transform.position;
            _nextFruit.GetComponent<Rigidbody>().isKinematic = false;
            StartCoroutine(Interval(_nextFruit.transform));
            _nextFruit = null;
        }
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
        if(_nextFruit)
            _nextFruit.GetComponent<Rigidbody>().isKinematic = true;
    }
    IEnumerator Interval(Transform trans)
    {
        while (true)
        {
            if(trans.IsDestroyed()) yield break;
            if(trans.position.y <= _stopIntervalLine)
            {
                _createFruitFlag = true;
                yield break;
            }
            yield return null;
        }
    }
}
