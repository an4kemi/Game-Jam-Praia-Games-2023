using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyVisibility : MonoBehaviour
{
    [SerializeField] private EnemySetting _setting;
    [SerializeField] private NavMeshAgent _agent;
    
    private Camera _playerCamera;

    private void OnValidate()
    {
        if (_setting != null && _agent != null) return;
        if (!TryGetComponent<Enemy>(out var enemyComponent)) return;
        _setting = enemyComponent.setting;
        _agent = enemyComponent.agent;
    }

    private void Start()
    {
        _playerCamera = Camera.main;
    }

    private void Update()
    {
        var screenPos = _playerCamera.WorldToScreenPoint(transform.position);
        var isVisibleOnScreen = screenPos.x > 0 && screenPos.x < Screen.width &&
                                 screenPos.y > 0 && screenPos.y < Screen.height && screenPos.z > 0;

        _agent.speed = isVisibleOnScreen ? _setting.Speed * GameConfig.AI_VISIBLE_SPEED_MULTIPLIER : _setting.Speed;
    }
}
