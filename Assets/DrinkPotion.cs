using System;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class DrinkPotion : MonoBehaviour
{
    public GameTime gameTime;
    public GameObject cap;
    public GameObject bottle;
    public Liquid liquid;

    public Vector3 moveFrom;
    public Vector3 moveTo;
    public float moveSpeed;

    public Vector3 capMoveFrom;
    public Vector3 capMoveTo;
    public float capMoveSpeed;
    public Ease capMoveEase;

    public Vector3 rotateFrom;
    public Vector3 rotateTo;
    public float rotateSpeed;

    public float drinkSpeed;
    public Ease drinkEase;
    
    public const string DRINK_MOVE_ID = "DrinkMoveTO";
    public const string DRINK_ROTATE_ID = "DrinkRotateTO";
    public const string DRINK_FILL_ID = "DrinkFill";
    public const string CAP_MOVE_ID = "CapMoveTO";

    public bool IsDrinking;

    public float RadiusRefillRate;
    
    [ContextMenu("Drink")]
    public void Drink(float multiplier)
    {
        if (IsDrinking) return;
        IsDrinking = true;
        DOTween.Kill(DRINK_MOVE_ID);
        DOTween.Kill(CAP_MOVE_ID);
        DOTween.Kill(DRINK_ROTATE_ID);
        DOTween.Kill(DRINK_FILL_ID);
        liquid.fillAmount = GameConfig.DREAM_POTION_FILL_AMOUNT;
        cap.SetActive(true);
        bottle.SetActive(true);
        
        cap.transform.localPosition = capMoveFrom;
        bottle.transform.localRotation = Quaternion.Euler(rotateFrom);
        bottle.transform.localPosition = moveFrom;
        bottle.transform.DOLocalMove(moveTo, moveSpeed).SetId(DRINK_MOVE_ID).OnComplete(() =>
        {
            cap.transform.DOLocalMove(capMoveTo, capMoveSpeed).SetId(CAP_MOVE_ID).SetEase(capMoveEase).OnComplete(() =>
            {
                cap.SetActive(false);
                bottle.transform.DOLocalRotate(rotateTo, rotateSpeed).SetId(DRINK_ROTATE_ID).OnComplete(() =>
                {
                    DOVirtual.Float(GameConfig.DREAM_POTION_FILL_AMOUNT, 1.5f, drinkSpeed,
                        value =>
                        {
                            gameTime.AddRadius(RadiusRefillRate * multiplier);
                            liquid.fillAmount = value;
                        }).SetId(DRINK_FILL_ID).SetEase(drinkEase).OnComplete(() =>
                    {
                        bottle.transform.DOLocalMove(moveFrom, moveSpeed * .5f).SetId(DRINK_MOVE_ID);
                        IsDrinking = false;
                    });
                });
            });
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsDrinking) return;
        if (!other.gameObject.CompareTag("Pickup")) return;
        if (!other.gameObject.TryGetComponent<Pickup>(out var pickup)) return;
        if (pickup.Type != PickupType.Drink) return;
        Drink(pickup.Multiplier);
        Destroy(other.gameObject);
    }
}
