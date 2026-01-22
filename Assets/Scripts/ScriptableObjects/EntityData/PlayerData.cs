using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Entity/Player")]
public class PlayerData : EntityData
{
    [SerializeField, Min(0)] private float _jumpForce;
    [SerializeField, Min(0)] private float _maxTimeRunning;
    [SerializeField, Min(1)] private float _runningSpeedMultiplier;
    
    [SerializeField, Min(0)] private int _maxStamina;
    [SerializeField, Min(0)] private int _returningStamina;

    [SerializeField, Min(0)] private int _wastingStamina;

    public float JumpForce => _jumpForce;
    public float MaxTimeRunning => _maxTimeRunning;
    public float RunningSpeedMultiplier => _runningSpeedMultiplier;

    public int MaxStamina => _maxStamina;
    public int ReturningStamina => _returningStamina;

    public int WastingStamina => _wastingStamina;
}