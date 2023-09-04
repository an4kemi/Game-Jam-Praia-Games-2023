using System;
using DG.Tweening;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    #if UNITY_EDITOR
    public MeshRenderer pictureRenderer;
    private void OnValidate()
    {
        if (Type == PickupType.Drink) return;
        if (Material != null)
        {
            pictureRenderer.material = Material;
        }
    }
#endif
    
    public PickupType Type;

    public float Multiplier;
    public Material Material;
    
    public Transform View;
    public Vector3 ViewMoveTo;
    public Vector3 RotateTo;

    public float MoveSpeed;
    public float RotateSpeed;

    public Ease JumpEase;
    public Ease MoveEase;
    
    
    private void Start()
    {
        View.DOLocalRotate(RotateTo, RotateSpeed).SetLoops(-1, LoopType.Incremental).SetEase(JumpEase);
        View.DOLocalMove(ViewMoveTo, MoveSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(MoveEase);
    }
}

public enum PickupType
{
    Drink,
    Memory,
}