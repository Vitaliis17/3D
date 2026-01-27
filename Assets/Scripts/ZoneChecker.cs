using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ZoneChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    
    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    public Collider[] ReadCollider()
        => Physics.OverlapBox(transform.position, _collider.size / 2, _collider.transform.rotation, _layerMask);
}