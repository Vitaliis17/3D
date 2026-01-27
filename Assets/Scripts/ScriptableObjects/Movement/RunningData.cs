using UnityEngine;

[CreateAssetMenu(fileName = "new RunningData", menuName = "Movement/Running")]
public class RunningData : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField, Min(0)] private int _wastingStamina;

    public float Speed => _speed;
    public int WastingStamina => _wastingStamina;
}