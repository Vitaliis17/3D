using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Entities/Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField, Min(0)] private float _speed;

    public float Speed => _speed;
}