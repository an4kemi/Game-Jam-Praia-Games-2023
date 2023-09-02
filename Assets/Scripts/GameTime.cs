using UnityEngine;

public class GameTime : MonoBehaviour
{
    public float DreamRadius => _dreamRadius;
    private float _dreamRadius;
    [SerializeField] private float _maxTime;
    [SerializeField] private float _time;
    
    [Header("Dream radius")]
    [SerializeField] private Material[] _materials;
    [SerializeField] private float _radiusMin;
    [SerializeField] private float _radiusMax;

    [SerializeField] private float _rationSpeed;
    [SerializeField] private float _rationIntensity;
    
    [Header("Foliage")]
    [SerializeField] 
    private Material[] _foliages;

    [SerializeField] 
    private Color _dreamColor;
    [SerializeField] 
    private Color _nightmareColor;
    
    private void Start()
    {
        _time = _maxTime;
    }

    private void Update()
    {
        if (_time >= 0) _time -= Time.deltaTime;
        
        var timeLeft = _time / _maxTime;
        _dreamRadius = Mathf.Lerp(_radiusMin, _radiusMax, timeLeft);
        // var ratio = Mathf.Abs(Mathf.Sin(Time.time * _rationSpeed)) / _rationIntensity;
        foreach (var material in _materials)
        {
            material.SetFloat("_Radius", _dreamRadius);
        }
        
        foreach (var foliage in _foliages)
        {
            foliage.SetColor("_TopColor", Color.Lerp(_nightmareColor, _dreamColor, _dreamRadius * .5f));
        }
    }
}
