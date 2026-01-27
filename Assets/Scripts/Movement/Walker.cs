using UnityEngine;

public class Walker : IMoveable
{
    private readonly float _speed;
    private readonly Rigidbody _rigidbody;
    private readonly Transform _transform;

    public Walker(float speed, Rigidbody rigidbody)
    {
        _speed = speed;
        _rigidbody = rigidbody;
        _transform = rigidbody.transform;
    }

    public void Move(Vector2 direction)
    {
        Vector3 localDirection = (_transform.forward * direction.y + _transform.right * direction.x) * _speed * Time.fixedDeltaTime;

        _rigidbody.MovePosition(_rigidbody.position + localDirection);
    }
}