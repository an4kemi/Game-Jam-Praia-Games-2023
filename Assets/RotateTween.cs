using DG.Tweening;
using UnityEngine;

public class RotateTween : MonoBehaviour
{
    public Vector3 rotateTo;
    public float duration;
    public LoopType loopType = LoopType.Incremental;
    public Ease easeType = Ease.Linear;
    void Start()
    {
        transform.DOLocalRotate(rotateTo, duration).SetLoops(-1, loopType).SetEase(easeType);
    }

}
