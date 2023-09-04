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
        var position = transform.position;
        Gizmos.DrawCube(position, GetComponent<BoxCollider>().size);
        Gizmos.DrawSphere(position + (transform.forward * 3), .5f);
    }
#endif
}