using System;
using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DrinkPotion : MonoBehaviour
{
    public GameTime gameTime;
    public GameObject cap;
    public GameObject bottle;
    public GameObject picture;
    public MeshRenderer pictureMaterial;
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
    public Vector3 pictureRotateFrom;
    public Vector3 pictureRotateTo;
    public float rotateSpeed;

    public float drinkSpeed;
    public Ease drinkEase;
    
    public const string DRINK_MOVE_ID = "DrinkMoveTO";
    public const string DRINK_ROTATE_ID = "DrinkRotateTO";
    public const string DRINK_FILL_ID = "DrinkFill";
    public const string CAP_MOVE_ID = "CapMoveTO";

    public bool IsDrinking;

    public float RadiusRefillRate;

    private int _pictureCount;

    private void Awake()
    {
        _pictureCount = 0;
        var pickups = FindObjectsByType<Pickup>(FindObjectsSortMode.None);
        foreach (var pickup in pickups)
        {
            if (pickup.Type == PickupType.Memory) _pictureCount++;
        }
    }

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
                        bottle.transform.DOLocalMove(moveFrom, moveSpeed * .5f).SetId(DRINK_MOVE_ID).OnComplete(() =>
                        {
                            bottle.SetActive(false);
                        });
                        IsDrinking = false;
                    });
                });
            });
        });
    }
    
    [ContextMenu("Take picture")]
    public void TakePicture ()
    {
        TakePicture(null);
    }

    public void TakePicture(Material material)
    {
        if (IsDrinking) return;
        IsDrinking = true;
        DOTween.Kill(DRINK_MOVE_ID);
        DOTween.Kill(CAP_MOVE_ID);
        DOTween.Kill(DRINK_ROTATE_ID);
        DOTween.Kill(DRINK_FILL_ID);
        picture.SetActive(true);
        pictureMaterial.material = material;
        
        picture.transform.localRotation = Quaternion.Euler(pictureRotateFrom);
        picture.transform.localPosition = moveFrom;
        picture.transform.DOLocalMove(moveTo, moveSpeed).SetId(DRINK_MOVE_ID).OnComplete(() =>
        {
            picture.transform.DOLocalRotate(pictureRotateTo, rotateSpeed).SetId(DRINK_ROTATE_ID).OnComplete(() =>
            {
                picture.transform.DOLocalMove(moveFrom, moveSpeed * .5f).SetId(DRINK_MOVE_ID).OnComplete(() =>
                {
                    picture.SetActive(false);
                });
                IsDrinking = false;
                _pictureCount--;
                if (_pictureCount <= 0)
                {
                    GameComplete();
                }
            });
        });
    }
    
    public GameObject winScreen;
    public string winTextContent;
    public TextMeshProUGUI winText;
    private int myIndex = 0;
    private float myDelay = 0.125f;
    
    [ContextMenu("Complete Game")]
    public void GameComplete()
    {
        Cursor.visible = true;
        winText.text = "";
        winScreen.SetActive(true);
        StartCoroutine(PlayText());
    }
    
    IEnumerator PlayText()
    {
        foreach (var c in winTextContent) 
        {
            winText.text += c;
            var wait = Random.Range(.03f, .09f);
            yield return new WaitForSeconds(wait);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (IsDrinking) return;
        if (!other.gameObject.CompareTag("Pickup")) return;
        if (!other.gameObject.TryGetComponent<Pickup>(out var pickup)) return;
        switch (pickup.Type)
        {
            case PickupType.Drink:
                Drink(pickup.Multiplier);
                break;
            case PickupType.Memory:
                TakePicture(pickup.Material);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        Destroy(other.gameObject);
    }
}
