using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;

    private Attacker _attacker;

    private void Awake()
        => _attacker = new(_weaponData.Damage);

    public void Attack()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _weaponData.Distance))
        {
            Health health = hit.transform.GetComponent<IHasHealth>().Health;
            _attacker.Attack(health);
        }
    }
}