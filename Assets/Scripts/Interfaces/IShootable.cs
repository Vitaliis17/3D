using UnityEngine;
using System;

public interface IShootable
{
    public event Func<Vector3, Bullet> Shooting;
}