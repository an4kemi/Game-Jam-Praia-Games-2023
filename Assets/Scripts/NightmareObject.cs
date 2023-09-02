using DefaultNamespace;
using UnityEngine;

public class NightmareObject : MonoBehaviour
{
    [SerializeField] private Collider _collider;

    private float _currentCooldown;

    // move to one place
    private void Update()
    {
        if (_currentCooldown <= 0)
        {
            _collider.isTrigger = false;
            return;
        }
        
        _currentCooldown -= Time.deltaTime;
    }

    public void SetCollisionActive(bool setActive)
    {
        _currentCooldown = GameConfig.NIGHTMARE_OBJECT_ENABLE_COLLISION_COOLDOWN;
        _collider.isTrigger = !setActive;
    }
}
