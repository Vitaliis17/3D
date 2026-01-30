using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class Player : Entity
{
    [SerializeField] private PlayerAnimationData _playerAnimationData;
    [SerializeField] private RotaterData _rotaterData;

    [SerializeField] private Transform _camera;

    [SerializeField] private Taker _taker;

    [SerializeField] private InputReader _reader;
    [SerializeField] private ZoneChecker _groundChecker;

    [SerializeField] private Mover _mover;

    private Rotater _rotater;
    private Jumper _jumper;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private AnimationController _animationController;
    private Coroutine _coroutine;

    public Stamina Stamina { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        _animator = GetComponent<Animator>();
        _animationController = new(_animator, _playerAnimationData);

        PlayerData data = Data as PlayerData;

        _rotater = new(_rotaterData.SensitivityY, _rotaterData.MaxRotationX, _rotaterData.MinRotationX);
        _jumper = new(_rigidbody, data.JumpForce);

        Stamina = new(data.MaxStamina, data.ReturningStamina);
        _mover.Initialize(Stamina, _rigidbody);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Stamina.SetFullValue();

        _reader.LookPerformed += RotateX;

        _reader.StartedJumping += Jump;
        _reader.StartedAttacking += _taker.Attack;
        _reader.StartedTaking += Take;

        _mover.StartingRunning += StopCoroutine;
        _mover.StartingRunning += _animationController.PlayRunning;

        _mover.StoppingRunning += RestoreStamina;
        _mover.StoppingRunning += _animationController.PlayWalking;

        _reader.StoppedMoving += _animationController.PlayIdle;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _reader.LookPerformed -= RotateX;

        _reader.StartedJumping -= Jump;
        _reader.StartedAttacking -= _taker.Attack;
        _reader.StartedTaking -= Take;

        _mover.StartingRunning -= StopCoroutine;
        _mover.StartingRunning -= _animationController.PlayRunning;

        _mover.StoppingRunning -= RestoreStamina;
        _mover.StoppingRunning -= _animationController.PlayWalking;

        _reader.StoppedMoving -= _animationController.PlayIdle;
    }

    protected override void Start()
    {
        base.Start();

        Stamina.SetFullValue();
    }

    private void RotateX(Vector2 direction)
        => _rotater.RotateX(transform, direction);

    private void Jump()
    {
        if (_groundChecker.ReadCollider().Length == 0)
            return;

        _jumper.Jump();
    }

    private void Take()
    {
        Ray ray = new(_camera.position, _camera.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, Data.TakingDistance, Data.TakingLayer) && hit.collider.TryGetComponent(out Weapon weapon))
            _taker.Take(weapon);
    }

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