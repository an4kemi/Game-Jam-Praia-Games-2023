using DG.Tweening;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool IsAttacking;

    public Transform View;

    private Vector3 RotateFrom;
    public Vector3 RotateTo;
    public float RotateSpeed;

    public Ease RotateToEase;
    public Ease RotateBackEase;

    public float AttackRadius;
    public float AttackRange;

    public LayerMask DamageLayer;
    
    private const string WEAPON_SWING_ID = "WeaponSwingID";

    private void Awake()
    {
        RotateFrom = View.localRotation.eulerAngles;
    }

    private void Update()
    {
        if (IsAttacking) return;
        if (!Input.GetMouseButtonDown(0)) return;
        IsAttacking = true;
        View.DOLocalRotate(RotateTo, RotateSpeed).SetId(WEAPON_SWING_ID).OnComplete(() =>
        {
            TryAttack();
            View.DOLocalRotate(RotateFrom, RotateSpeed * .5f).SetId(WEAPON_SWING_ID).OnComplete(() =>
            {
                IsAttacking = false;
            }).SetEase(RotateBackEase);
        }).SetEase(RotateToEase);
    }

    private void TryAttack()
    {
        var cast = Physics.SphereCastAll(transform.position, AttackRadius, transform.forward * AttackRange,
            AttackRange, DamageLayer);

        foreach (var hit in cast)
        {
            if (hit.transform.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.ChangeHealth();
            }
        }
    }
    
    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(transform.position, AttackRange);
    //
    //     RaycastHit hit;
    //     if (Physics.SphereCast(transform.position, AttackRadius, transform.forward * AttackRange, out hit, AttackRange, DamageLayer))
    //     {
    //         Gizmos.color = Color.green;
    //         Vector3 sphereCastMidpoint = transform.position + (transform.forward * hit.distance);
    //         Gizmos.DrawWireSphere(sphereCastMidpoint, AttackRadius);
    //         Gizmos.DrawSphere(hit.point, 0.1f);
    //         Debug.DrawLine(transform.position, sphereCastMidpoint, Color.green);
    //     }
    //     else
    //     {
    //         Gizmos.color = Color.red;
    //         Vector3 sphereCastMidpoint = transform.position + (transform.forward * (AttackRange-AttackRadius));
    //         Gizmos.DrawWireSphere(sphereCastMidpoint, AttackRadius);
    //         Debug.DrawLine(transform.position, sphereCastMidpoint, Color.red);
    //     }
    // }
}
