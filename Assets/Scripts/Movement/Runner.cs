using UnityEngine;

public class Runner : IMoveable
{
    private readonly int _wastingStamina;
    private readonly Stamina _stamina;

    private readonly float _speed;
    private readonly Rigidbody _rigidbody;
    private readonly Transform _transform;

    public Runner(Stamina stamina, float speed, Rigidbody rigidbody, int wastingStamina)
    {
        _stamina = stamina;

        _speed = speed;
        _rigidbody = rigidbody;
        _transform = rigidbody.transform;

        _wastingStamina = wastingStamina;
    }

    public void Move(Vector2 direction)
    {
        Vector3 localDirection = (_transform.forward * direction.y + _transform.right * direction.x) * _speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + localDirection);

        _stamina.SubtractValue(_wastingStamina);
    }
}