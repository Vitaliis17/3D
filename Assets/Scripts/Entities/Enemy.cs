using UnityEngine;

public class Enemy : MonoBehaviour, IHasHealth
{
    public Health Health { get; private set; }
}