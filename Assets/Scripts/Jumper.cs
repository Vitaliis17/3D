using UnityEngine;

public class Jumper
{
    private readonly Rigidbody _rigidbody;
    private readonly float _force;

    public Jumper(Rigidbody rigidbody, float force)
    {
        _rigidbody = rigidbody;
        _force = force;
    }
    
    public void Jump()
        => _rigidbody.AddForce(Vector3.up * _force);
}