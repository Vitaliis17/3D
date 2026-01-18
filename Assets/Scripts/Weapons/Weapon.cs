using UnityEngine;

public abstract class Weapon : MonoBehaviour, ITakeable
{
    [SerializeField] private ItemTakeData _data;

    public ItemTakeData Data => _data;

    public abstract void Attack();

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
        transform.SetLocalPositionAndRotation(_data.Position, _data.Rotation);
    }
}