using DefaultNamespace;
using UnityEngine;

public class CheckpointInteractor : MonoBehaviour
{
    private GameTime _gameTime;
    private Checkpoint _lastCheckpoint;
    private bool _isRespawning;

    private void Awake()
    {
        _gameTime = GetComponent<GameTime>();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            InvokeRespawn();
        }
    }
#endif

    private void FixedUpdate()
    {
        if (_isRespawning)
        {
            _isRespawning = false;
            MoveToLastCheckpoint();
        }
    }

    public void InvokeRespawn()
    {
        _isRespawning = true;
    }
    
    private void MoveToLastCheckpoint()
    {
        if (_lastCheckpoint == null) return;
        transform.position = _lastCheckpoint.transform.position;
        _gameTime.Reset();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hit.transform.CompareTag("Enemy")) return;
        InvokeRespawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            if (_gameTime.DreamRadius < GameConfig.AI_KILL_PLAYER_DREAM_RADIUS)
            {
                InvokeRespawn();
                return;
            }
        }
        
        if (!other.CompareTag("Checkpoint")) return;
        if (!other.TryGetComponent<Checkpoint>(out var checkpoint)) return;
        if (!checkpoint.IsTaken)
        {
            checkpoint.IsTaken = true;
            _lastCheckpoint = checkpoint;
        }
    }
}