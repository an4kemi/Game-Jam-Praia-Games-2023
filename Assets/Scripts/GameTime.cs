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
    [SerializeField] private Material[] _foliages;

    [SerializeField] private Color _dreamColor;
    [SerializeField] private Color _nightmareColor;

    [Header("Skybox")] 
    [SerializeField] private Camera _camera;
    [SerializeField] private Color _bgDreamColor;
    [SerializeField] private Color _bgNightmareColor;

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
            foliage.SetColor("_TopColor", Color.Lerp(_nightmareColor, _dreamColor, timeLeft));
        }
        
        _camera.backgroundColor = Color.Lerp(_bgNightmareColor, _bgDreamColor, timeLeft);
    }

    public void AddRadius(float value)
    {
        _time = Mathf.Min(_time + value, _maxTime);
    }
}