using UnityEngine;
using UnityEngine.AI;

public class EnemyVisibility : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    
    private Camera _playerCamera;
    
    public float normalSpeed = 3f;
    public float visibleSpeedMultiplier = 2f;

    private void Start()
    {
        _playerCamera = Camera.main;
    }

    private void Update()
    {
        var screenPos = _playerCamera.WorldToScreenPoint(transform.position);
        var isVisibleOnScreen = screenPos.x > 0 && screenPos.x < Screen.width &&
                                 screenPos.y > 0 && screenPos.y < Screen.height && screenPos.z > 0;

        _agent.speed = isVisibleOnScreen ? normalSpeed * visibleSpeedMultiplier : normalSpeed;
    }
}
