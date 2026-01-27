using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : Entity
{
    [SerializeField] private Transform _camera;
    [SerializeField] private RotaterData _rotaterData;

    [SerializeField] private Taker _taker;

    [SerializeField] private InputReader _reader;
    [SerializeField] private ZoneChecker _groundChecker;

    private Rotater _rotater;
    private Jumper _jumper;

    private IMoveable _moveable;

    private Walker _walker;
    private Runner _runner;

    private Rigidbody _rigidbody;
    private Coroutine _coroutine;

    public Stamina Stamina { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        PlayerData data = Data as PlayerData;

        _rotater = new(_rotaterData.SensitivityY, _rotaterData.MaxRotationX, _rotaterData.MinRotationX);
        _jumper = new(_rigidbody, data.JumpForce);

        Stamina = new(data.MaxStamina, data.ReturningStamina);

        _walker = new(data.Speed, _rigidbody);
        _runner = new(Stamina, data.Speed * data.RunningSpeedMultiplier, _rigidbody, data.WastingStamina);

        _moveable = _walker;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Stamina.SetFullValue();

        _reader.MovePerformed += Move;
        _reader.LookPerformed += RotateX;

        _reader.InputSystem.Player.Run.started += StartRunning;

        _reader.InputSystem.Player.Run.canceled += StopRunning;
        _reader.InputSystem.Player.Run.canceled += RestoreStamina;

        Stamina.Expired += RestoreStamina;
        Stamina.Expired += StopRunning;

        _reader.InputSystem.Player.Jump.started += Jump;
        _reader.InputSystem.Player.Attack.started += Attack;
        _reader.InputSystem.Player.Take.started += Take;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _reader.MovePerformed -= Move;
        _reader.LookPerformed -= RotateX;

        _reader.InputSystem.Player.Run.started -= StartRunning;

        _reader.InputSystem.Player.Run.canceled -= StopRunning;
        _reader.InputSystem.Player.Run.canceled -= RestoreStamina;

        Stamina.Expired -= RestoreStamina;
        Stamina.Expired -= StopRunning;

        _reader.InputSystem.Player.Jump.started -= Jump;
        _reader.InputSystem.Player.Attack.started -= Attack;
        _reader.InputSystem.Player.Take.started -= Take;
    }

    protected override void Start()
    {
        base.Start();

        Stamina.SetFullValue();
    }

    private void Move(Vector2 direction)
    {
        if (_moveable == _runner)
            StopCoroutine();

        _moveable.Move(direction);
    }

    private void RotateX(Vector2 direction)
        => _rotater.RotateX(transform, direction);

    private void Jump(InputAction.CallbackContext context)
    {
        if (_groundChecker.ReadCollider().Length == 0)
            return;

        _jumper.Jump();
    }

    private void Attack(InputAction.CallbackContext context)
        => _taker.Attack();

    private void Take(InputAction.CallbackContext context)
    {
        Ray ray = new(_camera.position, _camera.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, Data.TakingDistance, Data.TakingLayer) && hit.collider.TryGetComponent(out Weapon weapon))
            _taker.Take(weapon);
    }

    private void StartRunning(InputAction.CallbackContext context)
        => _moveable = _runner;

    private void StopRunning(InputAction.CallbackContext context)
        => StopRunning();

    private void StopRunning()
        => _moveable = _walker;

    private void RestoreStamina(InputAction.CallbackContext context)
        => RestoreStamina();

    private void RestoreStamina()
    {
        StopCoroutine();

        _coroutine = StartCoroutine(Stamina.Restore());
    }

    private void StopCoroutine()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}