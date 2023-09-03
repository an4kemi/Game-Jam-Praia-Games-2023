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

    [Header("Slope Alignment")]
    public LayerMask _groundLayerMask;
    public Transform model;
    public float alignmentSpeed;
    
    private void Start()
    {
        _currentDelay = GameConfig.AI_CHASE_UPDATE_DELAY;
        _gameTime = FindObjectOfType<GameTime>();
        _player = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        var isMoving = IsMoving();
        _animator.SetBool(ANIMATOR_IS_MOVING, isMoving);
        if (isMoving) AlignToGround();
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
    
    void AlignToGround()
    {
        var ray = new Ray(transform.position + Vector3.up, Vector3.down);
        if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, _groundLayerMask)) return;
        var newRotation = Quaternion.FromToRotation(model.up, hit.normal) * model.rotation;
        model.position = hit.point;
        model.rotation = Quaternion.Lerp(model.rotation, newRotation, alignmentSpeed * Time.deltaTime);

    }

    private bool IsMoving()
    {
        return _agent.velocity.sqrMagnitude > 0;
    }
}
