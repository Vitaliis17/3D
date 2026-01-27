using UnityEngine;

public interface IMoveable
{
    float Speed { get; }

    Rigidbody Rigidbody { get; }
    Transform Transform { get; }

    void Move(Vector2 direction);
}