using UnityEngine;

[CreateAssetMenu(fileName = "RotaterData", menuName = "Rotater")]
public class RotaterData : ScriptableObject
{
    [SerializeField, Min(0)] private float _sensitivityX;
    [SerializeField, Min(0)] private float _sensitivityY;

    [SerializeField] private float _maxRotationX;
    [SerializeField] private float _minRotationX;

    public float SensitivityX => _sensitivityX;
    public float SensitivityY => _sensitivityY;

    public float MaxRotationX => _maxRotationX;
    public float MinRotationX => _minRotationX;
}