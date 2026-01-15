using UnityEngine;
using System;

public class InputReader : MonoBehaviour
{
    public InputSystem InputSystem { get; private set; }

    private Vector2 _moveValue;
    private Vector2 _lookValue;

    public Action<Vector2> MovePerformed;
    public Action<Vector2> LookPerformed;

    private void Awake()
    {
        InputSystem = new();
        InputSystem.Player.Enable();
    }

    private void OnDisable()
        => InputSystem.Player.Disable();

    private void FixedUpdate()
    {
        _moveValue = InputSystem.Player.Move.ReadValue<Vector2>();
        _lookValue = InputSystem.Player.Look.ReadValue<Vector2>();

        if (_moveValue != Vector2.zero)
            MovePerformed?.Invoke(_moveValue);

        if(_lookValue != Vector2.zero)
            LookPerformed?.Invoke(_lookValue);
    }
}