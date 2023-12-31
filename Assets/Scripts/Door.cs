using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private DoorSetting _setting;
    [SerializeField] private Vector3 _open;
    private Vector3 _close;

    [SerializeField] private MeshRenderer[] _renderers;

    [SerializeField]
    private bool isOpen;

    [SerializeField] private GameObject[] _hints; 
    
    private void Awake()
    {
        _close = transform.localRotation.eulerAngles;

        if (_renderers != null && _renderers.Length > 0)
        {
            foreach (var renderer in _renderers)
            {
                renderer.material = _setting.Material;
            }
        }

        if (isOpen)
        {
            Open();
        }
    }

    public void Interact()
    {
        isOpen = !isOpen;
        transform.DOKill();
        transform.DOLocalRotate(isOpen ? _open : _close, isOpen ? _setting.OpenSpeed : _setting.CloseSpeed)
            .SetEase(isOpen ? _setting.OpenEase : _setting.CloseEase);
    }

    public void DisplayHints(bool display)
    {
        if (_hints == null || _hints.Length == 0) return;
        foreach (var hint in _hints)
        {
            hint.SetActive(display);
        }
    }

    [ContextMenu("Open")]
    public void Open()
    {
        transform.DOKill();
        transform.DOLocalRotate(_open, _setting.OpenSpeed).SetEase(_setting.OpenEase);
    }

    [ContextMenu("Close")]
    public void Close()
    {
        transform.DOKill();
        transform.DOLocalRotate(_close, _setting.CloseSpeed).SetEase(_setting.CloseEase);
    }
}
