using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    public bool IsTaken = false;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var color = Color.green;
        color.a = .25f;
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().size);
    }
#endif
}