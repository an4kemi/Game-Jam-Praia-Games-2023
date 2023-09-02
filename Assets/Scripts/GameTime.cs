using System;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    [SerializeField] private float _maxTime;
    [SerializeField] private float _time;
    
    [Header("Dream radius")]
    [SerializeField] private Material[] _materials;
    [SerializeField] private float _radiusMin;
    [SerializeField] private float _radiusMax;

    [SerializeField] private float _rationSpeed;
    [SerializeField] private float _rationIntensity;
    
    private void Start()
    {
        _time = _maxTime;
    }

    private void Update()
    {
        if (_time >= 0) _time -= Time.deltaTime;
        foreach (var material in _materials)
        {
            var timeLeft = _time / _maxTime;
            var ratio = Mathf.Abs(Mathf.Sin(Time.time * _rationSpeed)) / _rationIntensity;
            material.SetFloat("_Radius", Mathf.Lerp(_radiusMin, _radiusMax, timeLeft));
        }
    }
}
