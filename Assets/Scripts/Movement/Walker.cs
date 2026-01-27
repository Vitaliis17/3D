using UnityEngine;

public class Walker : IMoveable
{
    public Walker(float speed, Rigidbody rigidbody)
    {
        Speed = speed;
        Rigidbody = rigidbody;
        Transform = rigidbody.transform;
    }

    public float Speed { get; }
    public Rigidbody Rigidbody { get; }
    public Transform Transform { get; }

    public void Move(Vector2 direction)
    {
        Vector3 localDirection = (Transform.forward * direction.y + Transform.right * direction.x) * Speed * Time.fixedDeltaTime;

        Rigidbody.MovePosition(Rigidbody.position + localDirection);
    }
}