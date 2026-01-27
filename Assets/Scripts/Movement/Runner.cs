using UnityEngine;

public class Runner : IMoveable
{
    private readonly int _wastingStamina;
    private readonly Stamina _stamina;

    public Runner(Stamina stamina, float speed, Rigidbody rigidbody, int wastingStamina)
    {
        _stamina = stamina;

        Speed = speed;
        Rigidbody = rigidbody;
        Transform = rigidbody.transform;

        _wastingStamina = wastingStamina;
    }

    public float Speed { get; }
    public Rigidbody Rigidbody { get; }
    public Transform Transform { get; }

    public void Move(Vector2 direction)
    {
        Vector3 localDirection = (Transform.forward * direction.y + Transform.right * direction.x) * Speed * Time.fixedDeltaTime;
        Rigidbody.MovePosition(Rigidbody.position + localDirection);

        _stamina.SubtractValue(_wastingStamina);
    }
}