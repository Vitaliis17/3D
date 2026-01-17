using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Bullet")]
public class BulletData : ScriptableObject
{
    [SerializeField, Min(0)] private int _damage;
    [SerializeField, Min(0)] private float _lifeTime;

    public int Damage => _damage;
    public float LifeTime => _lifeTime;
}