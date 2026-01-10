using UnityEngine;

public class Mover
{
    private readonly float _speed;
    private readonly Rigidbody _rigidbody;

    public Mover(float speed, Rigidbody rigidbody)
    {
        _speed = speed;
        _rigidbody = rigidbody;
    }

    public void Move(Vector2 direction)
    {
        Vector3 localDirection = (_rigidbody.transform.forward * direction.y + _rigidbody.transform.right * direction.x) * Time.fixedDeltaTime * _speed;

        _rigidbody.MovePosition(_rigidbody.position + localDirection);
    }
}