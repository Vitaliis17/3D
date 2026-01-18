using UnityEngine;

public interface ITakeable
{
    ItemTakeData Data { get; }

    void SetParent(Transform parent);
}