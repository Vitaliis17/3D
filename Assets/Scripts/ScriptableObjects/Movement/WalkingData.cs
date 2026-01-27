using UnityEngine;

[CreateAssetMenu(fileName = "new WalkingData", menuName = "Movement/Walking")]
public class WalkingData : ScriptableObject
{
    [SerializeField] private float _speed;

    public float Speed => _speed;
}