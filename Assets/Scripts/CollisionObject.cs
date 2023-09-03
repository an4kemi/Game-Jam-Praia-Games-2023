using System;
using DefaultNamespace;
using UnityEngine;

public class CollisionObject : MonoBehaviour
{
    [SerializeField] private Collider _collider;

    private float _currentCooldown;
    private bool goalState;

    private void OnValidate()
    {
        if (_collider == null) _collider = GetComponent<Collider>();
        if (gameObject.layer == LayerMask.NameToLayer("Dream")) _collider.isTrigger = true;
    }

    // move to one place
    private void Update()
    {
        if (_currentCooldown <= 0)
        {
            _collider.isTrigger = goalState;
            return;
        }
        
        _currentCooldown -= Time.deltaTime;
    }

    public void SetCollisionActive(bool setActive)
    {
        _currentCooldown = GameConfig.NIGHTMARE_OBJECT_ENABLE_COLLISION_COOLDOWN;
        _collider.isTrigger = !setActive;
        goalState = setActive;
    }
}
