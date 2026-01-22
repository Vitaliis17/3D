using UnityEngine;

public abstract class Entity : MonoBehaviour, IHasHealth
{
    [SerializeField] private EntityData _data;

    protected EntityData Data => _data;

    public Health Health { get; protected set; }

    protected virtual void Awake()
        => Health = new(_data.MaxHealth);

    protected virtual void OnEnable()
    {
        Health.SetFullValue();

        Health.Expired += Die;
    }

    protected virtual void OnDisable()
        => Health.Expired -= Die;

    protected virtual void Start()
        => Health.SetFullValue();

    protected virtual void Die()
        => Destroy(gameObject);
}