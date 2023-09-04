using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEyes : MonoBehaviour
{
    [SerializeField] private GameTime _gameTime;
    [SerializeField]
    private RectTransform top;
    [SerializeField]
    private RectTransform bottom;

    [SerializeField] private float speed;
    
    
    private const float TARGET_Y_MAX = 1100;
    private const float TARGET_Y_DEFAULT = 0;
    private const float REFILL_AT = 1000;
    
    void Update()
    {
        var time = Time.deltaTime * speed;
        if (Input.GetMouseButton(0))
        {
            var yLerp = Mathf.Lerp(top.sizeDelta.y, TARGET_Y_MAX, time);
            top.sizeDelta = new Vector2(0, yLerp);
            bottom.sizeDelta = new Vector2(0, yLerp);

            if (yLerp >= REFILL_AT)
            {
                _gameTime.AddRadius(.085f);
            }
        }
        else
        {
            var yLerp = Mathf.Lerp(top.sizeDelta.y, TARGET_Y_DEFAULT, time * 3);
            top.sizeDelta = new Vector2(0, yLerp);
            bottom.sizeDelta = new Vector2(0, yLerp);
        }
    }
}
