using UnityEngine;

public class EnemySpawner : BaseSpawner<Enemy>
{
    [SerializeField] private Vector3[] positions;

    protected override void Awake()
    {
        base.Awake();

        foreach (Vector3 position in positions)
            Get(position);
    }
}