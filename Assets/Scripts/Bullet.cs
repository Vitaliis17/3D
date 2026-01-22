using UnityEngine;
using System;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour, ISpawnable
{
    [SerializeField] private BulletData _data;

    private Attacker _attacker;
    private Timer _timer;

    private Coroutine _coroutine;

    public Rigidbody Rigidbody { get; private set; }

    public event Action<ISpawnable> Releasing;

    private void Awake()
    {
        _attacker = new(_data.Damage);
        _timer = new();

        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Rigidbody.linearVelocity = Vector3.zero;
        StartTimer();

        _timer.TimeOvered += Die;
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _timer.TimeOvered -= Die;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IHasHealth healthObject))
            _attacker.Attack(healthObject.Health);

        Die();
    }

    private void StartTimer()
        => _coroutine = StartCoroutine(_timer.Wait(_data.LifeTime));

    private void Die()
        => Releasing?.Invoke(this);
}