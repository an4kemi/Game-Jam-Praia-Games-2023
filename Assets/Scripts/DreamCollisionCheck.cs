using UnityEngine;

public class DreamCollisionCheck : MonoBehaviour
{
    [SerializeField] private GameTime _gameTime;
    [SerializeField] private LayerMask _nightmareLayermask;
    [SerializeField] private LayerMask _dreamLayermask;
    [SerializeField] private Transform _target;

    [SerializeField] int _maxColliders = 20;
    private Collider[] _hitColliders;

    private void Awake()
    {
        _hitColliders = new Collider[_maxColliders];
    }

    private void Update()
    {
        var position = _target.position;
        var radius = _gameTime.DreamRadius;
        CheckCollision(position, radius, _nightmareLayermask, false);
        CheckCollision(position, radius, _dreamLayermask, true);
    }

    private void CheckCollision(Vector3 position, float radius, LayerMask layerMask, bool setActive)
    {
        int numColliders = Physics.OverlapSphereNonAlloc(position, radius, _hitColliders, layerMask);
        for (int i = 0; i < numColliders; i++)
        {
            var other = _hitColliders[i];
            if (!IsColliderFullyInsideSphere(other, position, radius))
            {
                continue;
            }

            if (other.TryGetComponent<CollisionObject>(out var collision))
            {
                collision.SetCollisionActive(setActive);
            }

            if (other.TryGetComponent<MeshActivationObject>(out var activation))
            {
                activation.SetActive(setActive);
            }
        }
    }

    bool IsColliderFullyInsideSphere(Collider collider, Vector3 position, float radius)
    {
        var bounds = collider.bounds;
        var closestPoint = bounds.ClosestPoint(position);
        var distance = Vector3.Distance(position, closestPoint);

        return distance + bounds.extents.magnitude <= _gameTime.DreamRadius;
    }
}