using UnityEngine;

public class Taker : MonoBehaviour
{
    [SerializeField] private Transform _container;

    private Weapon _weapon;

    public void Take(Weapon weapon)
    {
        _weapon = weapon;
        weapon.transform.SetParent(_container);
    }

    public void Attack()
    {
        if (_weapon == null)
            return;

        _weapon.Attack();
    }
}