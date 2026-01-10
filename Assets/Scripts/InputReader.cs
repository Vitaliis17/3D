using UnityEngine;
using System;

public class InputReader : MonoBehaviour
{
    private InputSystem _inputSystem;

    private Vector2 _moveValue;
    private Vector2 _lookValue;

    public Action<Vector2> MovePerformed;
    public Action<Vector2> LookPerformed;

    private void Awake()
    {
        _inputSystem = new();
        _inputSystem.Player.Enable();
    }

    private void FixedUpdate()
    {
        _moveValue = _inputSystem.Player.Move.ReadValue<Vector2>();
        _lookValue = _inputSystem.Player.Look.ReadValue<Vector2>();
        
        if (_moveValue != Vector2.zero)
            MovePerformed?.Invoke(_moveValue);

        if(_lookValue != Vector2.zero)
            LookPerformed?.Invoke(_lookValue);
    }
}