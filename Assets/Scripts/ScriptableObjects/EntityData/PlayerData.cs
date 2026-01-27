using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Entity/Player")]
public class PlayerData : EntityData
{
    [SerializeField, Min(0)] private float _jumpForce;
    
    [SerializeField, Min(0)] private int _maxStamina;
    [SerializeField, Min(0)] private int _returningStamina;

    public float JumpForce => _jumpForce;

    public int MaxStamina => _maxStamina;
    public int ReturningStamina => _returningStamina;
}