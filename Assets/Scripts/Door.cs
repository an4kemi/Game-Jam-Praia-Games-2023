using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private DoorSetting _setting;
    [SerializeField] private Vector3 _open;
    private Vector3 _close;

    [SerializeField] private MeshRenderer[] _renderers;

    private bool isOpen;

    [SerializeField] private GameObject[] _hints; 
    
    private void Awake()
    {
        isOpen = false;
        _close = transform.localRotation.eulerAngles;
        foreach (var renderer in _renderers)
        {
            renderer.material = _setting.Material;
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
