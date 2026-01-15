using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Entities/Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _jumpForce;

    public float Speed => _speed;
    public float JumpForce => _jumpForce;
}