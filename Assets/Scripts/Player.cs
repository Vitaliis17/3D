using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _reader;
    [SerializeField] private PlayerData _data;
    [SerializeField] private RotaterData _rotaterData;

    private Mover _mover;
    private Rotater _rotater;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        _mover = new(_data.Speed, _rigidbody);
        _rotater = new(_rotaterData.SensitivityY, _rotaterData.MaxRotationX, _rotaterData.MinRotationX);
    }
    
    private void OnEnable()
    {
        _reader.MovePerformed += _mover.Move;
        _reader.LookPerformed += RotateX;
    }

    private void OnDisable()
    {
        _reader.MovePerformed -= _mover.Move;
        _reader.LookPerformed -= RotateX;
    }

    private void RotateX(Vector2 direction)
        => _rotater.RotateX(transform, direction);
}
