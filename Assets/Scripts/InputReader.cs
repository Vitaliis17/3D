using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private InputSystem _inputSystem;

    private Vector2 _moveValue;
    private Vector2 _lookValue;

    public event Action<Vector2> MovePerformed;
    public event Action<Vector2> LookPerformed;

    public event Action StartedRunning;
    public event Action StoppedRunning;

    public event Action StartedJumping;
    public event Action StartedAttacking;
    public event Action StartedTaking;

    private void Awake()
        => _inputSystem = new();

    private void OnEnable()
    {
        _inputSystem.Player.Enable();

        _inputSystem.Player.Run.started += StartRunning;
        _inputSystem.Player.Run.canceled += StopRunning;

        _inputSystem.Player.Jump.started += StartJumping;
        _inputSystem.Player.Attack.started += StartAttacking;
        _inputSystem.Player.Take.started += StartTaking;
    }

    private void OnDisable()
    {
        _inputSystem.Player.Disable();

        _inputSystem.Player.Run.started -= StartRunning;
        _inputSystem.Player.Run.canceled -= StopRunning;

        _inputSystem.Player.Jump.started -= StartJumping;
        _inputSystem.Player.Attack.started -= StartAttacking;
        _inputSystem.Player.Take.started -= StartTaking;
    }

    private void Update()
    {
        _moveValue = _inputSystem.Player.Move.ReadValue<Vector2>();
        _lookValue = _inputSystem.Player.Look.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_moveValue.sqrMagnitude != 0)
            MovePerformed?.Invoke(_moveValue);

        if (_lookValue.sqrMagnitude != 0)
            LookPerformed?.Invoke(_lookValue);
    }

    private void StartRunning(InputAction.CallbackContext context)
        => StartedRunning?.Invoke();

    private void StopRunning(InputAction.CallbackContext context)
        => StoppedRunning?.Invoke();

    private void StartJumping(InputAction.CallbackContext context)
        => StartedJumping?.Invoke();

    private void StartAttacking(InputAction.CallbackContext context)
        => StartedAttacking?.Invoke();

    private void StartTaking(InputAction.CallbackContext context)
        => StartedTaking?.Invoke();
}