using UnityEngine;

public abstract class EntityData : ScriptableObject
{
    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private int _maxHealth;
    [SerializeField, Min(0)] private int _takingDistance;

    [SerializeField] private LayerMask _takingLayer;

    public float Speed => _speed;
    public int MaxHealth => _maxHealth;
    public int TakingDistance => _takingDistance;

    public LayerMask TakingLayer => _takingLayer;
}
