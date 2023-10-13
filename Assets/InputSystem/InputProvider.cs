using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputProvider : MonoBehaviour
{
    GameInputs _gameInputs;
    Vector2 _moveDir = default;
    void OnEnable()
    {
        _gameInputs.Enable();
    }
    void OnDisable()
    {
        _gameInputs.Dispose();
    }
    void Awake()
    {
        _gameInputs = new();
        _gameInputs.Player.Move.performed += context => Debug.Log(context.ReadValue<Vector2>());
        _gameInputs.Player.Move.canceled += context => Debug.Log("canceled");
    }
}
