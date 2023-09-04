using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Trigger : MonoBehaviour
{
    [FormerlySerializedAs("OnPlayerEnter")] public UnityEvent OnEnter;
    [FormerlySerializedAs("OnPlayerExit")] public UnityEvent OnExit;

    public string TargetTag = "Player";
    
    public bool ExecuteOnce;
    private bool _enterExecuted;
    private bool _exitExecuted;

    private void OnTriggerEnter(Collider other)
    {
        if (_enterExecuted) return;
        if (other.CompareTag(TargetTag))
        {
            if (ExecuteOnce) _enterExecuted = true;
            OnEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_exitExecuted) return;
        if (other.CompareTag(TargetTag))
        {
            if (ExecuteOnce) _exitExecuted = true;
            OnExit.Invoke();
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var color = Color.red;
        color.a = .25f;
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().size);
    }
#endif
}