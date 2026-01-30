using UnityEngine;
using System;

public class Mover : MonoBehaviour
{
    [SerializeField] private WalkingData _walkingData;
    [SerializeField] private RunningData _runningData;

    [SerializeField] private InputReader _reader;

    private IMoveable _moveable;

    private Walker _walker;
    private Runner _runner;

    private Stamina _stamina;

    public event Action StartingRunning;
    public event Action StoppingRunning;

    private void OnEnable()
    {
        _reader.StoppedMoving += StopRunning;
        _reader.MovePerformed += Move;

        _reader.StartedRunning += StartRunning;
        _reader.StoppedRunning += StopRunning;

        _stamina.Expired += StopRunning;
    }

    private void OnDisable()
    {
        _reader.StoppedMoving -= StopRunning;

        _reader.MovePerformed -= Move;

        _reader.StartedRunning -= StartRunning;
        _reader.StoppedRunning -= StopRunning;

        _stamina.Expired -= StopRunning;
    }

    public void Initialize(Stamina stamina, Rigidbody rigidbody)
    {
        _stamina = stamina;

        _walker = new(_walkingData.Speed, rigidbody);
        _runner = new(_stamina, _runningData.Speed, rigidbody, _runningData.WastingStamina);

        _moveable = _walker;
    }

    private void Move(Vector2 direction)
        => _moveable.Move(direction);

    private void StartRunning()
    {
        StartingRunning?.Invoke();

        _moveable = _runner;
    }

    private void StopRunning()
    {
        StoppingRunning?.Invoke();

        _moveable = _walker;
    }
}