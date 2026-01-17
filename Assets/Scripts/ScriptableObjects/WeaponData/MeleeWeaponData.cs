using UnityEngine;

[CreateAssetMenu(fileName = "MeleeData", menuName = "Weapon/Melee")]
public class MeleeWeaponData : ScriptableObject
{
    [SerializeField, Min(0)] private int _damage;
    [SerializeField, Min(0)] private float _distance;

    public int Damage => _damage;
    public float Distance => _distance;
}