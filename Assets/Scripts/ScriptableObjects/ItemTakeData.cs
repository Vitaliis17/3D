using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ItemData")]
public class ItemTakeData : ScriptableObject
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private Vector3 _angleRotation;

    private Quaternion _rotation;

    public Vector3 Position => _position;
    public Quaternion Rotation => _rotation;

    private void OnEnable()
        => _rotation = Quaternion.Euler(_angleRotation);
}