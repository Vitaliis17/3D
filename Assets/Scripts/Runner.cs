using UnityEngine;
using System.Collections;

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

        DeactivateRunning();
    }

    public float Speed { get; }
    public Rigidbody Rigidbody { get; }
    public Transform Transform { get; }

    public bool IsRunning { get; private set; }

    public void DeactivateRunning()
        => IsRunning = false;

    public void Move(Vector2 direction)
    {
        Vector3 localDirection = (Transform.forward * direction.y + Transform.right * direction.x) * Speed * Time.fixedDeltaTime;
        Rigidbody.MovePosition(Rigidbody.position + localDirection);

        _stamina.SubtractValue(_wastingStamina);
    }

    public IEnumerator WasteStamina(Stamina stamina)
    {
        IsRunning = true;

        WaitForFixedUpdate waiting = new();

        while (stamina.IsCurrentExpere() == false)
        {
            stamina.SubtractValue(_wastingStamina);

            yield return waiting;
        }

        DeactivateRunning();
    }
}