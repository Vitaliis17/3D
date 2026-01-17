using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Entity/Player")]
public class PlayerData : EntityData
{
    [SerializeField, Min(0)] private float _jumpForce;

    public float JumpForce => _jumpForce;
}