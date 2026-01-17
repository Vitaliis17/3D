using UnityEngine;

[CreateAssetMenu(fileName = "RangedData", menuName = "Weapon/Ranged")]
public class RangedWeaponData : ScriptableObject
{
    [SerializeField, Min(0)] private float _forceShooting;
    [SerializeField] private Vector3 _offset;

    public float ForceShooting => _forceShooting;
    public Vector3 Offset => _offset;
}