using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private RotaterData _rotaterData;
    [SerializeField] private InputReader _inputReader;

    private Rotater _rotater;

    private void Awake()
        => _rotater = new(_rotaterData.SensitivityY, _rotaterData.MaxRotationX, _rotaterData.MinRotationX);

    private void OnEnable()
        => _inputReader.LookPerformed += RotateY;

    private void OnDisable()
        => _inputReader.LookPerformed -= RotateY;

    private void RotateY(Vector2 rotation)
        => _rotater.RotateY(transform, rotation);
}