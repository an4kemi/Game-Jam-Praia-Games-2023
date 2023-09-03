using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MovingMesh : MonoBehaviour
{
    public MeshRenderer MeshRenderer;

    private void OnValidate()
    {
        if (MeshRenderer == null) MeshRenderer = GetComponent<MeshRenderer>();
    }
}