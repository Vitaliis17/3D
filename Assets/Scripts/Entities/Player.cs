using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : Entity
{
    [SerializeField] private RotaterData _rotaterData;

    [SerializeField] private InputReader _reader;

    [SerializeField] private Sword _sword;

    [SerializeField] private ZoneChecker _groundChecker;

    private Mover _mover;
    private Rotater _rotater;
    private Jumper _jumper;

    private Rigidbody _rigidbody;

    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        _mover = new(Data.Speed, _rigidbody);
        _rotater = new(_rotaterData.SensitivityY, _rotaterData.MaxRotationX, _rotaterData.MinRotationX);
        _jumper = new(_rigidbody, ((PlayerData)Data).JumpForce);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _reader.MovePerformed += _mover.Move;
        _reader.LookPerformed += RotateX;

        _reader.InputSystem.Player.Jump.performed += Jump;
        _reader.InputSystem.Player.Attack.performed += Attack;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _reader.MovePerformed -= _mover.Move;
        _reader.LookPerformed -= RotateX;

        _reader.InputSystem.Player.Jump.performed -= Jump;
        _reader.InputSystem.Player.Attack.performed -= Attack;
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
        => _sword.Attack();
}
