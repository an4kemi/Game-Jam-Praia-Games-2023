using UnityEngine;
using UnityEngine.Serialization;

public class SetMenuShaderValues : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    string shaderID = "_PositionMoving";

    [FormerlySerializedAs("_material")] [SerializeField]
    private Material[] _materials;
    
    private void Update()
    {
        var targetPosition = target.position;
        Shader.SetGlobalVector(shaderID, targetPosition);

        foreach (var material in _materials)
        {
            material.SetFloat("_Radius", transform.localScale.x);
        }
    }
}
