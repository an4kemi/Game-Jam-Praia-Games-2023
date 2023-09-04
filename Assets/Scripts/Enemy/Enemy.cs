using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public EnemySetting setting;
    [SerializeField] private Animator _animator;

    
    [Header("Health")]
    private int _currentHealth;
    
    private GameTime _gameTime;
    private bool _hasGameTime;
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
        _currentDelay = GameConfig.AI_CHASE_UPDATE_DELAY_MIN;
        _gameTime = FindObjectOfType<GameTime>();
        _hasGameTime = _gameTime != null;
        _player = FindObjectOfType<Player>().transform;

        _currentHealth = setting.Health;
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
        _currentDelay = Random.Range(GameConfig.AI_CHASE_UPDATE_DELAY_MIN, GameConfig.AI_CHASE_UPDATE_DELAY_MAX);
        
        var dreamRadius = GetDreamRadius();
        if (dreamRadius > GameConfig.AI_CHASE_DREAM_RADIUS_IGNORE)
        {
            agent.destination = RandomNavmeshLocation(10);
            return;
        }
        var distanceToPlayer = Vector3.Distance(_player.position, transform.position);
        if (distanceToPlayer > GameConfig.AI_CHASE_DREAM_DISTANCE_IGNORE)
        {
            agent.destination = RandomNavmeshLocation(10);
            return;
        }
        
        if (distanceToPlayer > dreamRadius)
        {
            agent.destination = _player.position;
        }
    }

    private float GetDreamRadius()
    {
        return _hasGameTime ? _gameTime.DreamRadius : 100;
    }
    
    private Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }
    
    private void AlignToGround()
    {
        var ray = new Ray(transform.position + Vector3.up, Vector3.down);
        if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, _groundLayerMask)) return;
        var newRotation = Quaternion.FromToRotation(model.up, hit.normal) * model.rotation;
        model.position = hit.point;
        model.rotation = Quaternion.Lerp(model.rotation, newRotation, alignmentSpeed * Time.deltaTime);

    }

    private bool IsMoving()
    {
        return agent.velocity.sqrMagnitude > 0;
    }
    
    public void ChangeHealth()
    {
        model.DOKill();
        model.DOPunchScale(setting.PunchScale, setting.PunchDuration);
        _currentHealth--;
        if (_currentHealth <= 0)
        {
            // todo play animation maybe lmao
            Destroy(gameObject);
        }
    }
}
