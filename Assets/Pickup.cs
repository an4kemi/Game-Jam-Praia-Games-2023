using DG.Tweening;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickupType Type;

    public float Multiplier;
    
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
}