using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseSpawner<T> : MonoBehaviour where T : Component, ISpawnable
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _container;

    private ObjectPool<T> _pool;

    protected virtual void Awake()
        => _pool = new(CreateElement, GetElement, ReleaseElement, DestroyElement);

    public virtual T Get(Vector3 position)
    {
        T element = _pool.Get();

        element.transform.position = position;
        element.Releasing += Release;

        return element;
    }

    protected virtual void Release(ISpawnable spawnableObject)
    {
        if (spawnableObject is T element)
        {
            element.Releasing -= Release;

            _pool.Release(element);
        }
    }

    private T CreateElement()
        => Instantiate(_prefab, _container);

    private void GetElement(T element)
        => element.gameObject.SetActive(true);

    private void ReleaseElement(T element)
        => element.gameObject.SetActive(false);

    private void DestroyElement(T element)
        => Destroy(element.gameObject);
}