using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class CinematicAgentMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private Vector3 _moveTo;
    [SerializeField] private float _destroyAfter;
    [SerializeField] private bool _destroyOnReach;
    
    private readonly int ANIMATOR_IS_MOVING = Animator.StringToHash("IsMoving");

    private void Awake()
    {
        _animator.SetBool(ANIMATOR_IS_MOVING, true);
    }

    public void Move()
    {
        _agent.SetDestination(_moveTo);
        if (_destroyOnReach)
        {
            DOVirtual.DelayedCall(_destroyAfter, () =>
            {
                Destroy(gameObject);
            });
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_moveTo, 1);
    }
}
