using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Enemy : Entity, ISpawnable
{
    [SerializeField] private Sword _sword;

    public event Action<ISpawnable> Releasing;

    protected override void Die()
        => Releasing?.Invoke(this);
}