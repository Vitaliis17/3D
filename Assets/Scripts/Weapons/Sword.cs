using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;

    private Attacker _attacker;

    private void Awake()
        => _attacker = new(_weaponData.Damage);

    public void Attack()
    {
        if (TryReadRaycastHit(out RaycastHit hit) && hit.transform.TryGetComponent(out IHasHealth healthObject))
            _attacker.Attack(healthObject.Health);
    }

    private bool TryReadRaycastHit(out RaycastHit hit)
        => Physics.Raycast(transform.position, transform.forward, out hit, _weaponData.Distance);
}