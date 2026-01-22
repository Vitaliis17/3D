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

    private Mover _mover;
    private Rotater _rotater;
    private Jumper _jumper;
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

        _mover = new(data.Speed, _rigidbody);
        _rotater = new(_rotaterData.SensitivityY, _rotaterData.MaxRotationX, _rotaterData.MinRotationX);
        _jumper = new(_rigidbody, data.JumpForce);
        _runner = new(data.WastingStamina, data.RunningSpeedMultiplier, data.MaxTimeRunning);

        Stamina = new(data.MaxStamina, data.ReturningStamina);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Stamina.SetFullValue();

        _reader.MovePerformed += Move;
        _reader.LookPerformed += RotateX;

        _reader.InputSystem.Player.Run.started += Run;

        _reader.InputSystem.Player.Run.canceled += StopRunning;
        _reader.InputSystem.Player.Run.canceled += RestoreStamina;

        Stamina.Expired += RestoreStamina;
        Stamina.Expired += _runner.DeactivateRunning;

        _reader.InputSystem.Player.Jump.started += Jump;
        _reader.InputSystem.Player.Attack.started += Attack;
        _reader.InputSystem.Player.Take.started += Take;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _reader.MovePerformed -= Move;
        _reader.LookPerformed -= RotateX;

        _reader.InputSystem.Player.Run.started -= Run;

        _reader.InputSystem.Player.Run.canceled -= StopRunning;
        _reader.InputSystem.Player.Run.canceled -= RestoreStamina;

        Stamina.Expired -= RestoreStamina;
        Stamina.Expired -= _runner.DeactivateRunning;

        _reader.InputSystem.Player.Jump.started -= Jump;
        _reader.InputSystem.Player.Attack.started -= Attack;
        _reader.InputSystem.Player.Take.started -= Take;
    }

    protected override void Start()
    {
        base.Start();

        Stamina.SetFullValue();
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

    private void Move(Vector2 direction)
    {
        const float BaseSpeedMultiplier = 1f;

        float speedMultiplier = _runner.IsRunning ? _runner.SpeedMultiplier : BaseSpeedMultiplier;

        _mover.Move(direction, speedMultiplier);
    }

    private void Run(InputAction.CallbackContext context)
    {
        StopCoroutine();

        _coroutine = StartCoroutine(_runner.WasteStamina(Stamina));
    }

    private void StopRunning(InputAction.CallbackContext context)
    {
        StopCoroutine();

        _runner.DeactivateRunning();
    }

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