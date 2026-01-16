using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IHasHealth
{
    [SerializeField] private InputReader _reader;

    [SerializeField] private PlayerData _data;
    [SerializeField] private RotaterData _rotaterData;

    [SerializeField] private Sword _sword;

    [SerializeField] private ZoneChecker _groundChecker;

    private Mover _mover;
    private Rotater _rotater;
    private Jumper _jumper;

    private Rigidbody _rigidbody;

    public Health Health { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        _mover = new(_data.Speed, _rigidbody);
        _rotater = new(_rotaterData.SensitivityY, _rotaterData.MaxRotationX, _rotaterData.MinRotationX);
        _jumper = new(_rigidbody, _data.JumpForce);

        Health = new(_data.MaxHealth);
    }

    private void OnEnable()
    {
        _reader.MovePerformed += _mover.Move;
        _reader.LookPerformed += RotateX;

        _reader.InputSystem.Player.Jump.performed += Jump;
        _reader.InputSystem.Player.Attack.performed += Attack;

        Health.Died += () => Destroy(gameObject);
    }

    private void OnDisable()
    {
        _reader.MovePerformed -= _mover.Move;
        _reader.LookPerformed -= RotateX;

        _reader.InputSystem.Player.Jump.performed -= Jump;
        _reader.InputSystem.Player.Attack.performed -= Attack;

        Health.Died -= () => Destroy(gameObject);
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
