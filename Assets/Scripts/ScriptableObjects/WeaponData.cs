using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField, Min(0)] private int _damage;
    [SerializeField, Min(0)] private float _distance;

    public int Damage => _damage;
    public float Distance => _distance;
}