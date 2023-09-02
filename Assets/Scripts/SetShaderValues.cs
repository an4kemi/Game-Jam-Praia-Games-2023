using UnityEngine;
using UnityEngine.Serialization;

public class SetShaderValues : MonoBehaviour
{
    MaterialPropertyBlock props;
    [SerializeField]
    Transform target;
    [SerializeField]
    string shaderID = "_PositionMoving";
    [SerializeField]
    float appearSpeed = 10f;
    [SerializeField]
    float disappearSpeed = 5f;
    [SerializeField]
    float radius = 12f;
    [SerializeField]
    bool keep = false;
 
    [SerializeField]
    float minRangeRandomOffset = -3f;
    [SerializeField]
    float maxRangeRandomOffset = 3f;

 
    [SerializeField]
    MeshRenderer[] objects;
    float[] values;
    float[] offsets;
    
    // Start is called before the first frame update
    void Start()
    {
        props = new MaterialPropertyBlock();
        values = new float[objects.Length];
        offsets = new float[objects.Length];
        SetRandomOffset();
        MeshBounds(); // hack to stop culling because the object is so far from its origin
    }
 
    // Update is called once per frame
    void Update()
    {
        var targetPosition = target.position;
        Shader.SetGlobalVector(shaderID, targetPosition); // set position to follow
        for (var i = 0; i < objects.Length; i++)
        {
            var offset = objects[i].transform.position - targetPosition;
            var sqrLen = offset.sqrMagnitude;
            if (sqrLen < radius * radius)
            {
                values[i] = Mathf.Lerp(values[i], 1, Time.deltaTime * appearSpeed);// set property float to 1 over time
            }            
            else if (!keep)
            {
                values[i] = Mathf.Lerp(values[i], 0, Time.deltaTime * disappearSpeed);// set property float to 0 over time if keep is not true
            }
            props.SetFloat("_Moved", values[i]);
            props.SetFloat("_RandomOffset", offsets[i]);
            objects[i].SetPropertyBlock(props);
        }
    }
 
    void SetRandomOffset()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            offsets[i] = Random.Range(minRangeRandomOffset, maxRangeRandomOffset);
         
        }
    }
 
    void MeshBounds()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            Mesh mesh = objects[i].GetComponent<MeshFilter>().mesh;
            mesh.bounds = new Bounds(Vector3.zero, 100f * Vector3.one);
        }
    }
}