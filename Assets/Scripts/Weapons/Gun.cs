using UnityEngine;
using System;

public class Gun : Weapon, ISpawnable, IShootable
{
    [SerializeField] private RangedWeaponData _weaponData;

    public event Action<ISpawnable> Releasing;
    public event Func<Vector3, Bullet> Shooting;

    public override void Attack()
    {
        Bullet bullet = Shooting?.Invoke(transform.position + transform.forward * transform.localScale.z + _weaponData.Offset);
        bullet.Rigidbody.AddForce(transform.forward * _weaponData.ForceShooting);
    }
}