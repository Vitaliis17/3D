using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _reader;

    [SerializeField] private PlayerData _data;
    [SerializeField] private RotaterData _rotaterData;

    [SerializeField] private ZoneChecker _groundChecker;

    private Mover _mover;
    private Rotater _rotater;
    private Jumper _jumper;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        _mover = new(_data.Speed, _rigidbody);
        _rotater = new(_rotaterData.SensitivityY, _rotaterData.MaxRotationX, _rotaterData.MinRotationX);
        _jumper = new(_rigidbody, _data.JumpForce);
    }

    private void OnEnable()
    {
        _reader.MovePerformed += _mover.Move;
        _reader.LookPerformed += RotateX;

        _reader.InputSystem.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        _reader.MovePerformed -= _mover.Move;
        _reader.LookPerformed -= RotateX;
    }

    private void RotateX(Vector2 direction)
        => _rotater.RotateX(transform, direction);

    private void Jump(InputAction.CallbackContext context)
    {
        if (_groundChecker.ReadCollider().Length == 0)
            return;

        _jumper.Jump();
    }
}
