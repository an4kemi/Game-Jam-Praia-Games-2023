using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    
    private GameTime _gameTime;
    private Transform _player;

    private float _currentDelay;
    private readonly int ANIMATOR_IS_MOVING = Animator.StringToHash("IsMoving");
    private readonly int ANIMATOR_TRIGGER_DEATH = Animator.StringToHash("Death");

    private void Start()
    {
        _currentDelay = GameConfig.AI_CHASE_UPDATE_DELAY;
        _gameTime = FindObjectOfType<GameTime>();
        _player = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        _animator.SetBool(ANIMATOR_IS_MOVING, IsMoving());
        if (_currentDelay > 0)
        {
            _currentDelay -= Time.deltaTime;
            return;
        }
        
        var distanceToPlayer = Vector3.Distance(_player.position, transform.position);
        Debug.Log($"{distanceToPlayer} > {_gameTime.DreamRadius}");
        if (distanceToPlayer > _gameTime.DreamRadius)
        {
            _agent.destination = _player.position;
        }
        

        _currentDelay = GameConfig.AI_CHASE_UPDATE_DELAY;
    }

    private bool IsMoving()
    {
        return _agent.velocity.sqrMagnitude > 0;
    }
}
