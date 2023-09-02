using UnityEngine;

public class NightmareCollisionCheck : MonoBehaviour
{
    [SerializeField] private GameTime _gameTime;
    [SerializeField] private LayerMask _layermask;
    [SerializeField] private Transform _target;
    
    [SerializeField] int _maxColliders = 10;
    private Collider[] _hitColliders;

    private void Awake()
    {
        _hitColliders = new Collider[_maxColliders];
    }

    private void Update()
    {
        var position = _target.position;
        var radius = _gameTime.DreamRadius;
        int numColliders = Physics.OverlapSphereNonAlloc(position, radius, _hitColliders, _layermask);
        Debug.Log("COLLIDERS: " + numColliders);
        for (int i = 0; i < numColliders; i++)
        {
            var other = _hitColliders[i];
            // todo check that object is within radius
            if (!IsColliderFullyInsideSphere(other, position, radius)) continue;
            if (other.TryGetComponent<NightmareObject>(out var component))
            {
                component.SetCollisionActive(false);
                Debug.Log("Set isTrigger!");
            }
        }
    }
    
    bool IsColliderFullyInsideSphere(Collider collider, Vector3 position, float radius)
    {
        Bounds bounds = collider.bounds;
        Vector3 closestPoint = bounds.ClosestPoint(position);
        float distance = Vector3.Distance(position, closestPoint);

        return distance + bounds.extents.magnitude <= _gameTime.DreamRadius;
    }
}