using DefaultNamespace;
using UnityEngine;

public class MeshActivationObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _renderer;

    private float _currentCooldown;
    private bool goalState;

    // move to one place
    private void Update()
    {
        if (_currentCooldown <= 0)
        {
            foreach (var renderer in _renderer)
            {
                renderer.enabled = goalState;
            }
            return;
        }
        
        _currentCooldown -= Time.deltaTime;
    }
    
    public void SetActive(bool setActive)
    {
        foreach (var renderer in _renderer)
        {
            renderer.enabled = setActive;
        }

        goalState = !setActive;
        _currentCooldown = GameConfig.NIGHTMARE_OBJECT_ENABLE_COLLISION_COOLDOWN;
    }
}
