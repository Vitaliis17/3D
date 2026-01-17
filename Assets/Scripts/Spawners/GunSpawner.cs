using UnityEngine;

public class GunSpawner : BaseSpawner<Gun>
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Vector3[] _positions;

    protected override void Awake()
    {
        base.Awake();

        foreach (Vector3 position in _positions)
            Get(position);
    }

    public override Gun Get(Vector3 position)
    {
        Gun gun = base.Get(position);

        gun.Shooting += _bulletSpawner.Get;

        return gun;
    }

    protected virtual void Release(Gun gun)
    {
        gun.Shooting -= _bulletSpawner.Get;

        base.Release(gun);
    }
}